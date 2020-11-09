using System.Collections.Generic;
using System.Threading.Tasks;
using ShitLeopard.Api.Models;

namespace ShitLeopard.Api.Contracts
{
    /// <summary>
    ///
    /// </summary>
    public interface IEpisodeService
    {
        /// <summary>
        /// Gets the episodes.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<EpisodeModel>> GetEpisodes(string pattern=null);
        /// <summary>
        /// Gets the episode.
        /// </summary>
        /// <param name="episodeId">The episode identifier.</param>
        /// <returns></returns>
        Task<EpisodeModel> GetEpisode(long episodeId);
    }
}