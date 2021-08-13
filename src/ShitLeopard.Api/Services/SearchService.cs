using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Entities;
using ShitLeopard.Api.Contracts;
using ShitLeopard.Api.Models;
using ShitLeopard.Common.Contracts;
using ShitLeopard.Common.Documents;
using ShitLeopard.Common.Models;

namespace ShitLeopard.Api.Services
{
    public class SearchService : ISearchService
    {
        private readonly IEntityContext _entityContext;
        private readonly ITagService _tagService;
        private readonly double _score = 0.9;

        public SearchService(IEntityContext entityContext, ITagService tagService)
        {
            _entityContext = entityContext;
            _tagService = tagService;
        }

        public async Task<IEnumerable<DialogModel>> SearchScriptLinesAsync(Question question)
        {
            var results = await SearchAsync(question);

            return results.Select(x => new DialogModel
            {
                Body = x.Body,
                DialogLineNumber = x.DialogLineNumber,
                End = x.End,
                EpisodeNumber = x.Episode.EpisodeNumber,
                EpisodeOffsetId = x.Episode.OffsetId,
                EpisodeTitle = x.Episode.Title,
                Id = x.ID,
                SeasonId = x.Episode.SeasonId,
                Start = x.Start,
                Synopsis = x.Episode.Synopsis,
                ShowName = x.Episode.Show?.Title
            });
        }

        public async Task<DialogModel> FindRandomSingleQuoteAsync()
        {
            var total = await DB.CountAsync<DialogDocument>();

            var id = 1;

            var result = await DB.Find<DialogDocument>().OneAsync(id.ToString());
            return _entityContext.Mapper.Map<DialogModel>(result);
        }

        public async Task<QuestionAnswer> AskQuestionAsync(Question question)
        {
            var results = await SearchAsync(question);

            var answer = new QuestionAnswer();
            answer.IsArray = true;
            answer.Match = true;
            answer.Question = question;
            answer.Answer = results.Select(x => new DialogModel
            {
                Body = x.Body,
                DialogLineNumber = x.DialogLineNumber,
                End = x.End,
                EpisodeNumber = x.Episode.EpisodeNumber,
                EpisodeOffsetId = x.Episode.OffsetId,
                EpisodeTitle = x.Episode.Title,
                Id = x.ID,
                SeasonId = x.Episode.SeasonId,
                Start = x.Start,
                Synopsis = x.Episode.Synopsis
            });
            return answer;
        }

        private async Task<IEnumerable<DialogDocument>> SearchAsync(Question question)
        {
            var test = await SearchEpisodesAsync(question);
            var limit = question.Limit <= 0 || question.Limit > 500 ? 500 : question.Limit;
            IEnumerable<DialogDocument> results = null;
            var pattern = question.FormatText();
            if (question.IsFuzzy)
            {
                results = await DB.Find<DialogDocument>()
                                            .Limit(limit)
                                            .Match(Search.Fuzzy, pattern)
                                            .ExecuteAsync();

                return results.SortByRelevance(pattern, x => x.Body, 50);
            }
            else
            {
                return await DB.Find<DialogDocument>()
                  .Limit(limit)
                 .Match(f => f.Text(pattern, new TextSearchOptions { CaseSensitive = false }))
                 .SortByTextScore()
                 .Sort(x => x.Episode.SeasonId, Order.Ascending)
                 .Sort(x => x.Episode.EpisodeNumber, Order.Ascending)

                 .ExecuteAsync();
            }
        }

        private async Task<IEnumerable<EpisodeDocument>> SearchEpisodesAsync(Question question)
        {
            var limit = question.Limit <= 0 || question.Limit > 500 ? 500 : question.Limit;
            IEnumerable<EpisodeDocument> results = null;
            var pattern = question.FormatText();
            if (question.IsFuzzy)
            {
                results = await DB.Find<EpisodeDocument>()
                                            .Limit(limit)
                                            .Match(Search.Fuzzy, pattern)
                                            .ExecuteAsync();

                return results.SortByRelevance(pattern, x => x.Body, 50);
            }
            else
            {
                results = await DB.Find<EpisodeDocument>()
                 .Limit(limit)
               .Match(f => f.Text(pattern, new TextSearchOptions { CaseSensitive = false }))

                .SortByTextScore()
                .Sort(x => x.SeasonId, Order.Ascending)
                .Sort(x => x.EpisodeNumber, Order.Ascending)
                .ExecuteAsync();

                return results;
            }
        }

        public async Task<IEnumerable<DialogModel>> GetAdjacentDialogTextAsync(string id, int distance)
        {
            distance = distance <= 0 || distance > 10 ? 1 : distance;
            var match = await DB.Find<DialogDocument>().OneAsync(id);
            if (match != null)
            {
                int lowerRange = (int)match.DialogLineNumber - distance;
                int upperRange = (int)match.DialogLineNumber + distance;

                lowerRange = lowerRange < 0 ? 0 : lowerRange;

                var query = from d in DB.Queryable<DialogDocument>()
                            where d.DialogLineNumber >= lowerRange && d.DialogLineNumber <= upperRange && d.Episode.Show.ID == match.Episode.Show.ID
                            select d;
                var result = await query.ToListAsync();
                return _entityContext.Mapper.MapCollection<DialogModel, DialogDocument>(result);
            }
            return Enumerable.Empty<DialogModel>();
        }

        public async Task<IEnumerable<QuoteModel>> FindQuotesAsync(Question question)
        {
            var limit = question.Limit <= 0 || question.Limit > 500 ? 500 : question.Limit;

            var pattern =  question.FormatText();

         
        

       var quoteSearchResults = await DB.Find<DialogDocument>()
                    .Limit(limit)
                   .Match(f => f.Text(pattern, new TextSearchOptions { CaseSensitive = false }))
                   .Project(x => x.MetaTextScore("Score"))
               
                   
   
                   .SortByTextScore()
                   .Sort(x => x.Episode.Show.Title, Order.Ascending)
                   .Sort(x => x.Episode.SeasonId, Order.Ascending)
                   .Sort(x => x.Episode.EpisodeNumber, Order.Ascending)
                   .ExecuteAsync();

            var distanceFilters = new List<FilterDefinition<DialogDocument>>();
            var endQuoteDistance = 3;
            var startQuoteDistance = 3;
            foreach ( var result in quoteSearchResults.GroupBy(x => x.Episode.ID))
            {
                var matches = result.OrderBy(x=>x.DialogLineNumber).ToList();
           
                var filter = matches.Select(q =>
                               Builders<DialogDocument>.Filter
                                       .Where(distanceFilter =>
                                                   distanceFilter.Episode.ID == result.Key &&
                                                   distanceFilter.DialogLineNumber >= q.DialogLineNumber - endQuoteDistance &&
                                                   distanceFilter.DialogLineNumber <= q.DialogLineNumber + startQuoteDistance));
                distanceFilters.AddRange(filter);
            }

            //quoteSearchResults =  quoteSearchResults.GroupBy(x => x.Episode.ID).SelectMany(z=>z).Where(s=>s.Score >= 1 ).Take(3).ToList();


            //await Option2(quoteSearchResults, question.Text);

            //var endQuoteDistance = 1;
            //var startQuoteDistance = 3;
            //var distanceFilters = map
            //        .Select(q =>
            //                    Builders<DialogDocument>.Filter
            //                            .Where(distanceFilter =>
            //                                        distanceFilter.Episode.ID == q.Key &&
            //                                        distanceFilter.DialogLineNumber >= q.DialogLineNumber - endQuoteDistance &&
            //                                        distanceFilter.DialogLineNumber <= q.DialogLineNumber + startQuoteDistance)


            //).ToArray();





            List<QuoteModel> quotes = new List<QuoteModel>();
            if (distanceFilters?.Any() == true)
            {
                var searchResults = await DB.Collection<DialogDocument>()
                        .Find(Builders<DialogDocument>
                        .Filter.Or(distanceFilters.ToArray()))
                        .ToListAsync();
                if (quoteSearchResults?.Any() == true)
                {
                    quotes = quoteSearchResults.GroupBy(x=>x.Episode.ID).Select(q =>
                    {
                        var quote = q.FirstOrDefault();
                        var model = new QuoteModel
                        {
                            Id = Guid.NewGuid().ToString(),
                            ShowName = quote.Episode.Show.Title,
                            Synopsis = quote.Episode.Synopsis,
                            SeasonId = quote.Episode.SeasonId,
                            EpisodeNumber = quote.Episode.EpisodeNumber,
                            EpisodeTitle = quote.Episode.Title,
                            EpisodeId = quote.Episode.ID,
                            Score = quote.Score
                        };

                        var matches = searchResults.OrderBy(x=>x.DialogLineNumber).Where(r => r.Episode.ID == model.EpisodeId);
                        if (matches?.Any() == true)
                        {
                            model.Lines.AddRange(matches.OrderBy(x => x.DialogLineNumber).Select(m => m.Body));
                        }
                        return model;
               


                    }).ToList();
                 
                }
            }

            return quotes;

        }

        //private async Task Option2(List<DialogDocument> quoteSearchResults, string pattern)
        //{
        //    // Create a new ML context, for ML.NET operations. It can be used for
        //    // exception tracking and logging, as well as the source of randomness.
        //    var mlContext = new MLContext();

        //    // Create an empty list as the dataset. The 'RemoveStopWords' does not
        //    // require training data as the estimator
        //    // ('CustomStopWordsRemovingEstimator') created by 'RemoveStopWords' API
        //    // is not a trainable estimator. The empty list is only needed to pass
        //    // input schema to the pipeline.
        //    var emptySamples = new List<TextData>();

        //    // Convert sample list to an empty IDataView.
        //    var emptyDataView = mlContext.Data.LoadFromEnumerable(emptySamples);

        //    // A pipeline for removing stop words from input text/string.
        //    // The pipeline first tokenizes text into words then removes stop words.
        //    // The 'RemoveStopWords' API ignores casing of the text/string e.g. 
        //    // 'tHe' and 'the' are considered the same stop words.
        //    var textPipeline = mlContext.Transforms.Text.TokenizeIntoWords("Words",
        //        "Text")
        //        .Append(mlContext.Transforms.Text.RemoveStopWords(
        //        "WordsWithoutStopWords", "Words", stopwords:
        //        new[] { "a", "the", "from", "by" }));

        //    // Fit to data.
        //    var textTransformer = textPipeline.Fit(emptyDataView);

        //    // Create the prediction engine to remove the stop words from the input
        //    // text /string.
        //    var predictionEngine = mlContext.Model.CreatePredictionEngine<TextData,
        //        TransformedTextData>(textTransformer);

        //    // Call the prediction API to remove stop words.
        //    var data = new TextData()
        //    {
        //        Text = "ML.NET's RemoveStopWords API " +
        //        "removes stop words from tHe text/string using a list of stop " +
        //        "words provided by the user."
        //    };

        //    var prediction = predictionEngine.Predict(data);

        //    // Print the length of the word vector after the stop words removed.
        //    Console.WriteLine("Number of words: " + prediction.WordsWithoutStopWords
        //        .Length);

        //    // Print the word vector without stop words.
        //    Console.WriteLine("\nWords without stop words: " + string.Join(",",
        //        prediction.WordsWithoutStopWords));
        //    foreach (var quote in quoteSearchResults.GroupBy(x => x.Episode.ID))
        //    {
        //        var currentEpisode = quote.Key;
        //        //this search can yield many hits within a single episode.  First group by the epside id to include include related/similar text.
        //        foreach( var line in quote.OrderBy(x => x.Start))
        //        {
        //            var text = line.Episode.Body.Substring(line.Episode.Body.IndexOfAny(pattern.ToCharArray()), 100);
        //            Console.WriteLine($"Position: {line.Start} Line:  {line.Body}");
        //        }
        //    }
        //}

        private class TextData
        {
            public string Text { get; set; }
        }

        private class TransformedTextData : TextData
        {
            public string[] WordsWithoutStopWords { get; set; }
        }
    }
}