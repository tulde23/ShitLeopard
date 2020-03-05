using System.Collections.Generic;
using System.Threading.Tasks;
using ShitLeopard.Api.Models;
using ShitLeopard.Common.Models;

namespace ShitLeopard.Api.Contracts
{
    public interface ISearchService
    {
        Task<IEnumerable<ScriptLineModel>> SearchScriptLinesAsync(Question question);

        /// <summary>
        /// Finds the random single quote.
        /// </summary>
        /// <returns></returns>
        Task<ScriptLineModel> FindRandomSingleQuoteAsync();


        /// <summary>
        /// Asks the question asynchronous.
        /// </summary>
        /// <param name="question">The question.</param>
        /// <returns></returns>
        Task<QuestionAnswer> AskQuestionAsync(Question question);
    }
}