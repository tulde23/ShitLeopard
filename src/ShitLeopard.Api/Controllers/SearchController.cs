using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShitLeopard.Api.Contracts;
using ShitLeopard.Api.Models;

namespace ShitLeopard.Api.Controllers
{
    /// <summary>
    /// A controller for searching scripts.
    /// </summary>
    /// <seealso cref="ShitLeopard.Api.Controllers.BaseController" />
    public class SearchController : ServiceController<ISearchService>
    {
        public SearchController(ILoggerFactory loggerFactory, ISearchService service) : base(loggerFactory, service)
        {
        }

        [HttpPost("LinesContaining")]
        public async Task<IEnumerable<ScriptLineModel>> LinesContaining([FromBody] Question question)
        {
            return await Service.SearchScriptLinesAsync(question);
        }

        [HttpPost]
        public async Task<dynamic> Ask([FromBody] Question question)
        {
            return await Service.FindRandomSingleQuoteAsync();
        }
    }
}