using System.Collections.Generic;
using System.Threading.Tasks;
using ShitLeopard.Api.Models;

namespace ShitLeopard.Api.Contracts
{
    public interface ISearchService
    {
        Task<IEnumerable<ScriptLineModel>> SearchScriptLinesAsync(Question question);

        /// <summary>
        /// Finds the random single quote.
        /// </summary>
        /// <returns></returns>
        Task<string> FindRandomSingleQuoteAsync();
    }
}