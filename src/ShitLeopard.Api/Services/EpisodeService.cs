using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Entities;
using ShitLeopard.Api.Contracts;
using ShitLeopard.Api.Models;
using ShitLeopard.Common.Contracts;
using ShitLeopard.Common.Documents;

namespace ShitLeopard.Api.Services
{
    public class EpisodeService : IEpisodeService
    {
        private readonly IEntityContext _entityContext;

        public EpisodeService(IEntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public async Task<EpisodeModel> GetEpisode(long episodeId)
        {
            var episode = await DB.Find<EpisodeDocument>().Match(x => x.Eq(f => f.EpisodeNumber, episodeId)).ExecuteFirstAsync();
            return _entityContext.Mapper.Map<EpisodeModel>(episode);
        }

        public async Task<IEnumerable<EpisodeModel>> GetEpisodes(string pattern = null)
        {
            if (!string.IsNullOrEmpty(pattern))
            {
                return _entityContext.Mapper.MapCollection<EpisodeModel, EpisodeDocument>((await DB.Find<EpisodeDocument>()

              .Match(f => f.Text(pattern, new TextSearchOptions { CaseSensitive = false }))
              .SortByTextScore()
              .Sort(x => x.SeasonId, Order.Ascending)
              .ExecuteAsync() ).SortByRelevance(pattern, x=>x.Title));
            }

            return _entityContext.Mapper.MapCollection<EpisodeModel, EpisodeDocument>(await DB.Find<EpisodeDocument>()
          .Sort(x => x.SeasonId, Order.Ascending)
          .ExecuteAsync());
        }
    }
}