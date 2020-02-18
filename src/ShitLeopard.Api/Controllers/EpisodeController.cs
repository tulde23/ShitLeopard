using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShitLeopard.Api.Contracts;
using ShitLeopard.Api.Models;

namespace ShitLeopard.Api.Controllers
{
    public class EpisodeController : ServiceController<IEpisodeService>
    {
        public EpisodeController(ILoggerFactory loggerFactory, IEpisodeService service) : base(loggerFactory, service)
        {
        }

        /// <summary>
        /// Retrieves all Episodes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(List<EpisodeModel>))]
        public async Task<IEnumerable<EpisodeModel>> GetEpisodes()
        {
            return await Service.GetEpisodes();
        }

        /// <summary>
        /// Retrieves all Episodes.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{episodeId:long}")]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(EpisodeModel))]
        public async Task<EpisodeModel> GetEpisode([FromRoute] long episodeId)
        {
            return await Service.GetEpisode(episodeId);
        }

        
    }
}