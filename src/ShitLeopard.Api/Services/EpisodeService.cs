using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using ShitLeopard.Api.Contracts;
using ShitLeopard.Api.Models;
using ShitLeopard.Common.Contracts;
using ShitLeopard.Common.Documents;

namespace ShitLeopard.Api.Services
{
    public class EpisodeService : BaseService, IEpisodeService
    {
        public EpisodeService(ILoggerFactory loggerFactory, IMongoProvider contextProvider, IMapper mapper) : base(loggerFactory, contextProvider, mapper)
        {
        }

        public async Task<EpisodeModel> GetEpisode(long episodeId)
        {
            var episodesCollection = ContextProvider.GetEpisodesCollection();
            var episode = await episodesCollection.Find(Builders<EpisodeDocument>.Filter.Eq(x => x.Id, episodeId)).FirstOrDefaultAsync();
            return Mapper.Map<EpisodeModel>(episode);
        }

        public async Task<IEnumerable<EpisodeModel>> GetEpisodes(string pattern = null)
        {
            var episodesCollection = ContextProvider.GetEpisodesCollection();
            var filter = Builders<EpisodeDocument>.Filter.Empty;
            if (!string.IsNullOrEmpty(pattern))
            {
                filter = Builders<EpisodeDocument>.Filter.Text(pattern, new TextSearchOptions { CaseSensitive = false });
            }
            var results = await episodesCollection.Find(filter).ToListAsync();
            return Mapper.MapCollection<EpisodeModel, EpisodeDocument>(results);
        }
    }
}