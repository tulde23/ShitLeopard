using System.Collections.Generic;
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
        [TrackQuery("question")]
        [HttpPost()]
        public async Task<IEnumerable<QuoteModel>> FindQuotes([FromBody] Question question)
        {
            return await Service.FindQuotesAsync(question);
        }

    }
}