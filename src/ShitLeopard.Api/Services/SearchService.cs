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

        public SearchService(IEntityContext entityContext, ITagService tagService)
        {
            _entityContext = entityContext;
            _tagService = tagService;
        }

        public async Task<IEnumerable<DialogModel>> SearchScriptLinesAsync(Question question)
        {
            await _tagService.SaveTagAsync(new TagsModel
            {
                Category = "DialogSearch",
                Frequency = 0,
                Name = question.Text
            });

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
                Synopsis = x.Episode.Synopsis
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
                            where d.DialogLineNumber >= lowerRange && d.DialogLineNumber <= upperRange
                            select d;
                var result = await query.ToListAsync();
                return _entityContext.Mapper.MapCollection<DialogModel, DialogDocument>(result);
            }
            return Enumerable.Empty<DialogModel>();
        }
    }
}