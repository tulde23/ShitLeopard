using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShitLeopard.Api.Contracts;
using ShitLeopard.Api.Models;
using ShitLeopard.DataLayer.Entities;

namespace ShitLeopard.Api.Controllers
{
    public class SeasonController : ServiceController<ISeasonService>
    {
        public SeasonController(ILoggerFactory loggerFactory, ISeasonService service) : base(loggerFactory, service)
        {
        }

        /// <summary>
        /// Retrieves all Seasons.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(List<Season>))]
        public async Task<IEnumerable<SeasonModel>> Get()
        {
            return await Service.ListAsync();
        }

        /// <summary>
        /// Retrieves all Seasons.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{seasonId:long}")]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(Season))]
        public async Task<SeasonModel> GetSeason([FromRoute] long seasonId)
        {
            return await Service.GetSeasonAsync(seasonId);
        }
    }
}