using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShitLeopard.Api.Contracts;
using ShitLeopard.Api.Models;
using ShitLeopard.Common.Contracts;
using ShitLeopard.Common.Models;
using ShitLeopard.DataLayer.Entities;

namespace ShitLeopard.Api.Services
{
    public class SearchService : BaseService, ISearchService
    {
        private readonly string _defaultComment = "Does that answer your question pussy tits?";
        private readonly string _defaultMiss = "How the fuck are you so stupid?";

        private readonly List<string> _terms = new List<string>()
            {
                "shit*",
                "fuck*",
                "cock*",
                "dope*"
            };

        private readonly ITagService _tagService;
        private readonly INaturalLanguageService _naturalLanguageService;

        public SearchService(ILoggerFactory loggerFactory, Func<ShitLeopardContext> contextProvider, IMapper mapper, ITagService tagService, INaturalLanguageService naturalLanguageService) : base(loggerFactory, contextProvider, mapper)
        {
            _tagService = tagService;
            _naturalLanguageService = naturalLanguageService;
        }

        public async Task<QuestionAnswer> AskQuestionAsync(Question question)
        {
            long occurences = 0;
            var result = await _naturalLanguageService.ParseSentenceAsync(question.Text);
            if (!result.IsQuestion)
            {
                var lines = await SearchScriptLinesAsync(question);
                return new QuestionAnswer
                {
                    Question = question,
                    Answer = lines,
                    Match = lines.Any(),
                    IsArray = true,
                    Comment = lines.Any() ? _defaultComment : _defaultMiss
                };
            }
            using (var context = ContextProvider())
            {
                if (question.Text.Trim().Split(" ".ToCharArray()).Length > 1)
                {
                    occurences = await context.CountOccurencesOfPhrase(question.Text);
                    return new QuestionAnswer
                    {
                        Question = question,
                        Answer = FormatAnswer("phrase", question.Text, occurences),
                        Match = occurences > 0,
                        Comment = occurences > 0 ? _defaultComment : _defaultMiss
                    };
                }
                else
                {
                    occurences = await context.CountOccurencesOfSingleWord(question.Text);
                    return new QuestionAnswer
                    {
                        Question = question,
                        Answer = FormatAnswer("word", question.Text, occurences),
                        Match = occurences > 0,
                        Comment = occurences > 0 ? _defaultComment : _defaultMiss
                    };
                }
            }
        }

        public async Task<ScriptLineModel> FindRandomSingleQuoteAsync()
        {
            using (var context = ContextProvider())
            {
                var query = new StringBuilder("select top 1 * From ScriptLine WHERE ");

                query.Append(string.Join(" OR ", _terms.Select(x => $" CONTAINS (body, '\"{x})\"' )")));
                query.Append(" ORDER BY NewID()");

                using (var c = context.Database.GetDbConnection())
                {
                    c.Open();

                    return (await c.QueryAsync<ScriptLineModel>(query.ToString()))?.FirstOrDefault();
                }
            }
        }

        public async Task<IEnumerable<ScriptLineModel>> SearchScriptLinesAsync(Question question)
        {
            using (var context = ContextProvider())
            {
                var query = @"
sELECT
      FT_TBL.*, E.Id as EpisodeId, E.SeasonId, E.Title as EpisodeTitle, E.OffsetId
FROM ScriptLine AS FT_TBL INNER JOIN
   CONTAINSTABLE (ScriptLine,
      Body,
      @SearchTerm,
      150
   ) AS KEY_TBL
   ON FT_TBL.Id = KEY_TBL.[KEY]
   inner join Script S on S.Id = FT_TBL.ScriptId
   inner join Episode E on E.Id = S.Id
";

                await _tagService.SaveTagAsync(new TagsModel
                {
                    Name = question.Text,
                    Category = "Search"
                });
                using (var c = context.Database.GetDbConnection())
                {
                    c.Open();

                    var p = new DynamicParameters();
                    p.Add("SearchTerm", $"\"{question.Text}\"");
                    return await c.QueryAsync<ScriptLineModel>(query, p);
                }
            }
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