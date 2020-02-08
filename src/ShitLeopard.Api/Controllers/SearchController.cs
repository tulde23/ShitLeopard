using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

        [HttpGet("LinesContaining")]
        public async Task<IEnumerable<ScriptLine>> LinesContaining([FromQuery] string term)
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
            var  query = "select  * from ScriptLine  where contains( Body, '@p0')";
            

            return await Context.ScriptLine.FromSqlRaw(query, term).AsNoTracking().ToListAsync();
        }
    }
}