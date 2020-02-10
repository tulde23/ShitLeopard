using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShitLeopard.Api.Models;
using ShitLeopard.DataLayer.Entities;

namespace ShitLeopard.Api.Controllers
{
    /// <summary>
    /// A controller for searching scripts.
    /// </summary>
    /// <seealso cref="ShitLeopard.Api.Controllers.BaseController" />
    public class SearchController : BaseController
    {
        public SearchController(ILogger<BaseController> logger, ShitLeopardContext shitLeopardContext) : base(logger, shitLeopardContext)
        {
        }

        [HttpPost("LinesContaining")]
        public async Task<IEnumerable<ScriptLine>> LinesContaining([FromBody] Question question)
        {
            /**
             *     phrase = $"FORMSOF(FREETEXT, \"{phrase}\")";

        var query = _dataContext.Example
            .FromSql(@"SELECT [Id]
                      ,[Sentence]
                    FROM [dbo].[Example]
                    WHERE CONTAINS(Sentence, @p0)", phrase)
            .AsNoTracking();

        return query.ToList();
             * */
            var query = "select top 10 * from ScriptLine  where contains( Body, '\"@Term\"')";
            using (var c = Context.Database.GetDbConnection())
            {
                c.Open();

                var test = await c.QueryAsync<ScriptLine>(query, new { Term = question.Text });
                return test;
            }
        }

        [HttpPost]
        public async Task<dynamic> Ask([FromBody] Question question)
        {
            using (var c = Context.Database.GetDbConnection())
            {
                c.Open();

                return await c.ExecuteScalarAsync<string>("select top 1 Body From ScriptLine   where contains(body, 'shit*') OR contains(body,'fuck*') ORDER BY NewID()");
            }
        }
    }
}