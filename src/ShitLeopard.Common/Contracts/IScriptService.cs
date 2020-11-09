using System.Collections.Generic;
using System.Threading.Tasks;
using ShitLeopard.Api.Models;

namespace ShitLeopard.Api.Contracts
{
    public interface IScriptService
    {
        /// <summary>
        /// Gets the script.
        /// </summary>
        /// <param name="episodeId">The episode identifier.</param>
        /// <returns></returns>
        Task<ScriptModel> GetScriptAsync(long episodeId);




        /// <summary>
        /// Gets the script lines.
        /// </summary>
        /// <param name="scriptId">The script identifier.</param>
        /// <param name="includeAll">The include all.</param>
        /// <returns></returns>
        Task<IEnumerable<ScriptLineModel>> SearchScriptLinesAsync(string pattern);

        /// <summary>
        /// Gets the script words.
        /// </summary>
        /// <param name="scriptLineId">The script line identifier.</param>
        /// <returns></returns>
        Task<IEnumerable<ScriptWordModel>> GetScriptWordsForLineAsync(long scriptLineId);

        /// <summary>
        /// Sets the script line character.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task UpdateScriptLineAsync(ScriptLineModel model);
    }
}