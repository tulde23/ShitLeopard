using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShitLeopard.DataLayer.Entities;

namespace ShitLeopard.Api.Controllers
{
    public class SeasonController : BaseController
    {
        public SeasonController(ILogger<BaseController> logger, ShitLeopardContext shitLeopardContext) : base(logger, shitLeopardContext)
        {
        }

        /// <summary>
        /// Retrieves all Seasons.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(List<Season>))]
        public async Task<IEnumerable<Season>> Get()
        {
            return await Context.Season.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Retrieves all Seasons.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{seasonId:long}")]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(Season))]
        public async Task<Season> GetSeason([FromRoute] long seasonId)
        {
            return await Context.Season.AsNoTracking().Include(x => x.Episode).ThenInclude(x => x.Script).FirstOrDefaultAsync(x => x.Id == seasonId);
        }
    }
}