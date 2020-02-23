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
using ShitLeopard.DataLayer.Entities;

namespace ShitLeopard.Api.Services
{
    public class SearchService : BaseService, ISearchService
    {
        private readonly List<string> _terms = new List<string>()
            {
                "shit*",
                "fuck*",
                "cock*",
                "dope*"
            };
        private readonly ITagService _tagService;

        public SearchService(ILoggerFactory loggerFactory, Func<ShitLeopardContext> contextProvider, IMapper mapper, ITagService tagService) : base(loggerFactory, contextProvider, mapper)
        {
            _tagService = tagService;
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
      FT_TBL.*, E.Id as EpisodeId, E.SeasonId, E.Title as EpisodeTitle
FROM ScriptLine AS FT_TBL INNER JOIN
   CONTAINSTABLE (ScriptLine,
      Body,
      @SearchTerm,
      50
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
    }
}