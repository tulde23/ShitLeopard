using System.Threading.Tasks;
using ShitLeopard.Api.Models;

namespace ShitLeopard.Api.Contracts
{
    public interface IQuoteService
    {
        /// <summary>
        /// Gets the random quote.
        /// </summary>
        /// <returns></returns>
        Task<QuoteModel> GetRandomQuoteAsync();

        /// <summary>
        /// Saves the quote.
        /// </summary>
        /// <param name="quoteModel">The quote model.</param>
        /// <returns></returns>
        Task SaveQuoteAsync(QuoteModel quoteModel);

        /// <summary>
        /// Deletes the quote asynchronous.
        /// </summary>
        /// <param name="quoteId">The quote identifier.</param>
        /// <returns></returns>
        Task DeleteQuoteAsync(long quoteId);
    }
}