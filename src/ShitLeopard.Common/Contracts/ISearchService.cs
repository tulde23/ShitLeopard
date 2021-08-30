using System.Collections.Generic;
using System.Threading.Tasks;
using ShitLeopard.Api.Models;
using ShitLeopard.Common.Models;

namespace ShitLeopard.Api.Contracts
{
    public interface ISearchService
    {
        
        /// <summary>
        /// Finds the quotes asynchronous.
        /// </summary>
        /// <param name="question">The question.</param>
        /// <returns></returns>
        Task<IEnumerable<QuoteModel>> FindQuotesAsync(Question question);

    }
}