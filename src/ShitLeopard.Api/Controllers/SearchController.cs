using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShitLeopard.Api.Contracts;
using ShitLeopard.Api.Filters;
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

        [InboundRequestInspectorFilter]
        [HttpPost("LinesContaining")]
        public async Task<IEnumerable<ScriptLineModel>> LinesContaining([FromBody] Question question)
        {
            if (string.IsNullOrEmpty(question?.Text)  || question.Text.Length > 255)
            {
                return Enumerable.Empty<ScriptLineModel>();
            }

            return await Service.SearchScriptLinesAsync(question);
        }

    }
}