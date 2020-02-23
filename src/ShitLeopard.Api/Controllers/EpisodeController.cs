using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShitLeopard.Api.Contracts;
using ShitLeopard.Api.Models;
using ShitLeopard.Common.Models;

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
        [HttpGet("GroupBySeason")]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(List<EpisodeModel>))]
        public async Task<IEnumerable<EpisodeGroupingModel>> GetEpisodesBySeason()
        {
            var episodes = await Service.GetEpisodes();
            var items = new List<EpisodeGroupingModel>();
            foreach (var item in episodes.GroupBy(x => x.SeasonId))
            {
                items.Add(new EpisodeGroupingModel
                {
                    Season = $"Season {item.Key}",
                    SeasonId = item.Key,
                    Episodes = new List<EpisodeModel>(item)
                });
            }
            return items;
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