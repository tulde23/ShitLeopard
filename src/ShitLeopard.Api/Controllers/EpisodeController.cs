using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShitLeopard.Api.Contracts;
using ShitLeopard.Api.Models;
using ShitLeopard.Api.ViewModels;
using ShitLeopard.Common.Contracts;
using ShitLeopard.Common.Models;

namespace ShitLeopard.Api.Controllers
{

    public class EpisodeController : ServiceController<IEpisodeService, IShowService>
    {
        public EpisodeController(ILoggerFactory loggerFactory, IEpisodeService service, IShowService showService) : base(loggerFactory, service, showService)
        {
        }

        /// <summary>
        /// Retrieves all Episodes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetEpisodes([FromQuery] string pattern = null)
        {
            var viewModel = new EpisodeViewModel
            {
                Episodes = new List<EpisodeModel>(await Service.GetEpisodes(pattern)),
                Shows = new List<ShowModel>(await Service2.GetShowsAsync())
            };
            return View("Episodes", viewModel);
        }

        /// <summary>
        /// Retrieves all Episodes.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GroupBySeason/{showid}")]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(List<EpisodeModel>))]
        public async Task<EpisodeGroupingViewModel> GetEpisodesBySeason([FromRoute] string showid, [FromQuery] string pattern = null)
        {
            var episodes = await Service.GetEpisodes(showid, pattern);
            var items = new List<EpisodeGroupingModel>();

            var show = await  this.Service2.GetShowAsync(showid);
            foreach (var item in episodes.GroupBy(x => x.SeasonId))
            {
                items.Add(new EpisodeGroupingModel
                {
         
                    Season = $"Season {item.Key}",
                    SeasonId = item.Key,
                    Episodes = new List<EpisodeModel>(item)
                
                });
            }

            items = items.OrderBy(x => int.Parse(x.SeasonId)).ToList();
            var model = new EpisodeGroupingViewModel { ShowName = show.Title,  Episodes = items, SeasonCount = items.Count, EpisodeCount = items.SelectMany(x=>x.Episodes).Count() };
            return model;
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