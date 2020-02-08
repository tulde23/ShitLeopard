using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShitLeopard.DataLayer.Entities;

namespace ShitLeopard.Api.Controllers
{
    public class ScriptController : BaseController
    {
        public ScriptController(ILogger<BaseController> logger, ShitLeopardContext shitLeopardContext) : base(logger, shitLeopardContext)
        {
        }

        /// <summary>
        /// Retrieves all Episodes.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{scriptId:long}")]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(Script))]
        public async Task<Script> GetScript(long scriptId)
        {
            return await Context.Script.AsNoTracking()
                .Include(x => x.Episode)
                .Include(x => x.ScriptLine)
                .ThenInclude(y => y.ScriptWord)
                .SingleOrDefaultAsync(x => x.Id == scriptId);
        }

        /// <summary>
        /// Retrieves all lines in a script.
        /// </summary>
        /// <param name="scriptId">The script identifier.</param>
        /// <param name="includeAll">The include all.</param>
        /// <returns></returns>
        [HttpGet("Lines/{scriptId:long}")]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(Script))]
        public async Task<IEnumerable<ScriptLine>> GetScriptLines([FromRoute] long scriptId, [FromQuery] bool? includeAll=null)
        {
            return await Context.ScriptLine.AsNoTracking().Where(x => x.ScriptId == scriptId).ToListAsync();
        }

        /// <summary>
        /// Retrieves all words in a script line.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Words/{scriptLineId:long}")]
        [Produces("application/json")]
       
        [ProducesResponseType(200, Type = typeof(Script))]
        public async Task<IEnumerable<ScriptWord>> GetScriptWords(long scriptLineId)
        {
            return await Context.ScriptWord.AsNoTracking().Where(x => x.ScriptLineId == scriptLineId).ToListAsync();
        }
    }
}