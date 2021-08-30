using System.Collections.Generic;
using System.Threading.Tasks;
using ShitLeopard.Api.Contracts;
using ShitLeopard.Api.Models;
using ShitLeopard.Common.Contracts;
using ShitLeopard.Common.Documents;
using System.Linq;

namespace ShitLeopard.Api.Services
{
    public class ElasticSearchService : ISearchService
    {
        private readonly IElasticSearchConnectionProvider _elasticSearchConnectionProvider;
        private readonly ITagService _tagService;

        public ElasticSearchService(IElasticSearchConnectionProvider elasticSearchConnectionProvider, ITagService tagService)
        {
            _elasticSearchConnectionProvider = elasticSearchConnectionProvider;
            _tagService = tagService;
        }

        public async Task<IEnumerable<QuoteModel>> FindQuotesAsync(Question question)
        {
            var limit = question.Limit <= 0 || question.Limit > 500 ? 500 : question.Limit;

            var pattern = question.FormatText();
            var searchResults = await this._elasticSearchConnectionProvider.Client.SearchAsync<EpisodeDocument>(
                s => s

                        .Query(q => q
                            .MatchPhrase(m => m
                                .Field(f => f.Body)
                                    .Query(question.Text)))
                        .Highlight(f => f.PreTags("<em class=highlight>").PostTags("</em>").Fields((p) => p.Field(z => z.Body))));

            return searchResults.Hits.Select(x => new QuoteModel
            {

                Lines = new List<string>(x.Highlight["body"]),
                EpisodeNumber = x.Source.EpisodeNumber,
                EpisodeId = x.Source.DocumentId,
                EpisodeOffsetId = x.Source.OffsetId,
                EpisodeTitle = x.Source.Title,
                SeasonId = x.Source.SeasonNumber.ToString(),
                Synopsis = x.Source.Synopsis,
                ShowName = x.Source.ShowTitle

            }) ;
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