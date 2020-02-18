using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShitLeopard.Api.Contracts;
using ShitLeopard.Api.Models;

namespace ShitLeopard.Api.Controllers
{
    public class QuoteController : ServiceController<IQuoteService>
    {
        public QuoteController(ILoggerFactory loggerFactory, IQuoteService service) : base(loggerFactory, service)
        {
        }

        [HttpGet]
        public async Task<QuoteModel> RandomQuote(long id)
        {
            return await Service.GetRandomQuoteAsync();
        }

        [HttpPost]
        public async Task<QuoteModel> Update([FromBody] QuoteModel model)
        {
            await Service.SaveQuoteAsync(model);
            return model;
        }

        /// <summary>
        /// Retrieves all Episodes.
        /// </summary>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task Upvote(long id)
        {
            await Service.SaveQuoteAsync(new Models.QuoteModel
            {
                ScriptLineId = id
            });
        }
    }
}