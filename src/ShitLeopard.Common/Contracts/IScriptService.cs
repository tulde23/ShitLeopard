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
        /// <param name="scriptId">The script identifier.</param>
        /// <returns></returns>
        Task<ScriptModel> GetScript(long scriptId);

        /// <summary>
        /// Gets the script lines.
        /// </summary>
        /// <param name="scriptId">The script identifier.</param>
        /// <param name="includeAll">The include all.</param>
        /// <returns></returns>
        Task<IEnumerable<ScriptLineModel>> GetScriptLines(long scriptId, bool? includeAll = null);

        /// <summary>
        /// Gets the script words.
        /// </summary>
        /// <param name="scriptLineId">The script line identifier.</param>
        /// <returns></returns>
        Task<IEnumerable<ScriptWordModel>> GetScriptWords(long scriptLineId);

        /// <summary>
        /// Sets the script line character.
        /// </summary>
        /// <param name="scriptLineId">The script line identifier.</param>
        /// <param name="characterId">The character identifier.</param>
        /// <returns></returns>
        Task SetScriptLineCharacter(long scriptLineId, long characterId);
    }
}