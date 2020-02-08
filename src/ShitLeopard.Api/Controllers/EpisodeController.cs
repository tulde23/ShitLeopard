using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShitLeopard.DataLayer.Entities;

namespace ShitLeopard.Api.Controllers
{
    public class EpisodeController : BaseController
    {
        public EpisodeController(ILogger<BaseController> logger, ShitLeopardContext shitLeopardContext) : base(logger, shitLeopardContext)
        {
        }

        /// <summary>
        /// Retrieves all Episodes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(List<Season>))]
        public async Task<IEnumerable<Episode>> GetEpisodes()
        {
            return await Context.Episode.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Retrieves all Episodes.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{episodeId:long}")]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(Episode))]
        public async Task<Episode> GetEpisode([FromRoute] long episodeId)
        {
            return await Context.Episode.AsNoTracking( )
                .Include(x => x.Script)
                .ThenInclude(x => x.ScriptLine)
                .FirstOrDefaultAsync(x => x.Id == episodeId);
        }
    }
}