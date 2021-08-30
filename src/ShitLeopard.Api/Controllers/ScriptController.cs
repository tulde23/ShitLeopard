using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShitLeopard.Api.Contracts;
using ShitLeopard.Api.Models;

namespace ShitLeopard.Api.Controllers
{
    public class ScriptController : ServiceController<IScriptService>
    {
        public ScriptController(ILoggerFactory loggerFactory, IScriptService service) : base(loggerFactory, service)
        {
        }

        /// <summary>
        /// Retrieves all Episodes.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{episodeId:long}")]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(ScriptModel))]
        public async Task<ScriptModel> GetScript(long episodeId)
        {
            return await Service.GetScriptAsync(episodeId);
        }

        /// <summary>
        /// Retrieves all lines in a script.
        /// </summary>
        /// <param name="scriptId">The script identifier.</param>
        /// <param name="includeAll">The include all.</param>
        /// <returns></returns>
        [HttpGet("Lines")]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(ScriptLineModel))]
        public async Task<IEnumerable<ScriptLineModel>> GetScriptLineModels([FromQuery] string pattern = null)
        {
            return await Service.SearchScriptLinesAsync(pattern);
        }

        /// <summary>
        /// Retrieves all words in a script line.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Words/{scriptLineId:long}")]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(ScriptWordModel))]
        public async Task<IEnumerable<ScriptWordModel>> GetScriptWords(long scriptLineId)
        {
            return await Service.GetScriptWordsForLineAsync(scriptLineId);
        }

        /// <summary>
        /// Retrieves all words in a script line.
        /// </summary>
        /// <returns></returns>
        [HttpPost("ScripLine")]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(ScriptWordModel))]
        public async Task UpdateScriptLine([FromBody] ScriptLineModel model)
        {
            await Service.UpdateScriptLineAsync(model);
        }
    }
}