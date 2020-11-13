using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShitLeopard.Api.Contracts;
using ShitLeopard.Api.Filters;
using ShitLeopard.Api.Models;
using ShitLeopard.Common.Models;

namespace ShitLeopard.Api.Controllers
{
    /// <summary>
    /// A controller for searching dialog.
    /// </summary>
    /// <seealso cref="ShitLeopard.Api.Controllers.BaseController" />
    public class SearchController : ServiceController<ISearchService>
    {
        public SearchController(ILoggerFactory loggerFactory, ISearchService service) : base(loggerFactory, service)
        {
        }

        /// <summary>
        /// Finds dialog containing the supplied text.
        /// </summary>
        /// <param name="question">The question.</param>
        /// <returns></returns>
        [InboundRequestInspectorFilter]
        [HttpPost()]
        public async Task<IEnumerable<DialogModel>> LinesContaining([FromBody] Question question)
        {
            if (string.IsNullOrEmpty(question?.Text) || question.Text.Length > 255)
            {
                return Enumerable.Empty<DialogModel>();
            }

            return await Service.SearchScriptLinesAsync(question);
        }

        /// <summary>
        /// Finds dialog containing the supplied text.
        /// </summary>
        /// <param name="question">The question.</param>
        /// <returns></returns>

        [HttpGet("{id}")]
        public async Task<IEnumerable<DialogModel>> AdjacentText([FromRoute] string id, [FromQuery] int? distance = 2)
        {
            return await Service.GetAdjacentDialogTextAsync(id, distance ?? 2);
        }
    }
}