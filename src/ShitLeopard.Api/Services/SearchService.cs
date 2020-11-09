using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using ShitLeopard.Api.Contracts;
using ShitLeopard.Api.Models;
using ShitLeopard.Common.Contracts;
using ShitLeopard.Common.Documents;
using ShitLeopard.Common.Models;

namespace ShitLeopard.Api.Services
{
    public class SearchService : BaseService, ISearchService
    {
        private readonly string _defaultComment = "Does that answer your question pussy tits?";
        private readonly string _defaultMiss = "How the fuck are you so stupid?";

        private static Random rand = new Random((int)DateTime.Now.Ticks);

        private readonly List<string> _terms = new List<string>()
            {
                "shit*",
                "fuck*",
                "cock*",
                "dope*"
            };

        private readonly ITagService _tagService;

        public SearchService(ILoggerFactory loggerFactory, IMongoProvider contextProvider, IMapper mapper, ITagService tagService) : base(loggerFactory, contextProvider, mapper)
        {
            _tagService = tagService;
        }

        public async Task<QuestionAnswer> AskQuestionAsync(Question question)
        {
            var collection = ContextProvider.GetLinesCollection();
            var results = await collection.Find(Builders<LineDocument>.Filter.Text(question.Text, new TextSearchOptions { CaseSensitive = false })).ToListAsync();

            var answer = new QuestionAnswer();
            answer.IsArray = true;
            answer.Match = true;
            answer.Question = question;
            answer.Answer = results.Take(1000).ToList();
            return answer;
        }

        public async Task<ScriptLineModel> FindRandomSingleQuoteAsync()
        {
            var collection = ContextProvider.GetLinesCollection();
            var total = await collection.CountDocumentsAsync(Builders<LineDocument>.Filter.Empty);

            var id = rand.Next(1,(int)total);

            var result = await collection.FindAsync(x => x.Id == id);
            return Mapper.Map<ScriptLineModel>(result);
        }

        public async Task<IEnumerable<ScriptLineModel>> SearchScriptLinesAsync(Question question)
        {
            var collection = ContextProvider.GetLinesCollection();
            var results = await collection.Find(Builders<LineDocument>.Filter.Text(question.Text, new TextSearchOptions { CaseSensitive = false })).ToListAsync();

            await _tagService.SaveTagAsync(new TagsModel
            {
                Name = question.Text,
                Category = "Search"
            });
            return Mapper.MapCollection<ScriptLineModel, LineDocument>(results);
        }

        private static string FormatAnswer(string type, string text, long occurrances)
        {
            var formatted = String.Format("{0:n0}", occurrances);
            if (occurrances > 0)
            {
                return $" The {type} '{text}' occurs {formatted} times";
            }
            else if (occurrances == 1)
            {
                return $"The {type} '{text}' occurs once.";
            }
            else
            {
                return $"The {type} '{text}' produces no matches.";
            }
        }
    }
}