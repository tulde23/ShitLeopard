using System.Collections.Generic;
using System.Threading.Tasks;
using ShitLeopard.Api.Contracts;
using ShitLeopard.Api.Models;

namespace ShitLeopard.Api.Services
{
    public class EpisodeService : IEpisodeService
    {
        public Task<EpisodeModel> GetEpisode(long episodeId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<EpisodeModel>> GetEpisodes(string showId, string pattern = null)
        {
            throw new System.NotImplementedException();
        }
    }
}