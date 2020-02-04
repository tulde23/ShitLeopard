using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShitLeopard.Entities;

namespace ShitLeopard.Api.Controllers
{
    [Route("api/[controller]")]
    public class DataController : Controller
    {
        private readonly ILogger<DataController> _logger;
        private readonly ShitLeopardContext _shitLeopardContext;

        public DataController(ILogger<DataController> logger, ShitLeopardContext shitLeopardContext)
        {
            _logger = logger;
            _shitLeopardContext = shitLeopardContext;
        }

        /// <summary>
        /// Retrieves all Seasons.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Seasons")]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(List<Season>))]
        public async Task<IEnumerable<Season>> Get()
        {
            return await _shitLeopardContext.Season.ToListAsync();
        }

        /// <summary>
        /// Retrieves all Seasons.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Seasons/{seasonId:long}")]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(Season))]
        public async Task<Season> GetSeason([FromRoute] long seasonId)
        {
            return await _shitLeopardContext.Season.Include(x => x.Episode).ThenInclude(x=>x.Script).FirstOrDefaultAsync(x => x.Id == seasonId);
        }

        /// <summary>
        /// Retrieves all Episodes.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Episodes")]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(List<Season>))]
        public async Task<IEnumerable<Episode>> GetEpisodes()
        {
            return await _shitLeopardContext.Episode.ToListAsync();
        }
        /// <summary>
        /// Retrieves all Episodes.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Episodes/{episodeId:long}")]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(Episode))]

        public async Task<Episode> GetEpisode([FromRoute] long episodeId)
        {
            return await _shitLeopardContext.Episode.Include(x => x.Script).ThenInclude(x => x.ScriptLine).FirstOrDefaultAsync(x => x.Id == episodeId);
        }
    }
}