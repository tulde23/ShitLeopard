using System.Collections.Generic;
using System.Threading.Tasks;
using ShitLeopard.Api.Models;
using ShitLeopard.Common.Models;

namespace ShitLeopard.Api.Contracts
{
    public interface ISearchService
    {
        Task<IEnumerable<DialogModel>> SearchScriptLinesAsync(Question question);

        /// <summary>
        /// Finds the random single quote.
        /// </summary>
        /// <returns></returns>
        Task<DialogModel> FindRandomSingleQuoteAsync();


        /// <summary>
        /// Finds the quotes asynchronous.
        /// </summary>
        /// <param name="question">The question.</param>
        /// <returns></returns>
        Task<IEnumerable<QuoteModel>> FindQuotesAsync(Question question);


        /// <summary>
        /// Asks the question asynchronous.
        /// </summary>
        /// <param name="question">The question.</param>
        /// <returns></returns>
        Task<QuestionAnswer> AskQuestionAsync(Question question);

        /// <summary>
        /// Gets the adjacent dialog text by distance.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="distance">The distance.</param>
        /// <returns></returns>
        Task<IEnumerable<DialogModel>> GetAdjacentDialogTextAsync(string id, int distance);
    }
}