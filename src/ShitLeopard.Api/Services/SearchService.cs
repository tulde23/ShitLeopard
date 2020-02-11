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

        public SearchService(ILoggerFactory loggerFactory, Func<ShitLeopardContext> contextProvider, IMapper mapper) : base(loggerFactory, contextProvider, mapper)
        {
        }

        public async Task<string> FindRandomSingleQuoteAsync()
        {
            using (var context = ContextProvider())
            {
                var query = new StringBuilder("select top 1 Body From ScriptLine WHERE ");

                query.Append(string.Join(" OR ", _terms.Select(x => $" CONTAINS (body, '\"{x})\"' )")));
                query.Append(" ORDER BY NewID()");

                using (var c = context.Database.GetDbConnection())
                {
                    c.Open();

                    return await c.ExecuteScalarAsync<string>(query.ToString());
                }
            }
        }

        public async Task<IEnumerable<ScriptLineModel>> SearchScriptLinesAsync(Question question)
        {
            using (var context = ContextProvider())
            {
                var query = "select top 10  * from ScriptLine where ID in (select ScriptLineId from  ScriptWord where contains( word, @SearchTerm) )";
                using (var c = context.Database.GetDbConnection())
                {
                    c.Open();

                    var p = new DynamicParameters();
                    p.Add("SearchTerm", $"\"{question.Text}\"");
                    return Mapper.MapCollection<ScriptLineModel, ScriptLine>(await c.QueryAsync<ScriptLine>(query, p));
                }
            }
        }
    }
}