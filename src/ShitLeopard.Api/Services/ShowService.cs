using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Entities;
using ShitLeopard.Common.Contracts;
using ShitLeopard.Common.Documents;
using ShitLeopard.Common.Models;
using MongoDB.Driver.Linq;
using MongoDB.Driver;

namespace ShitLeopard.Api.Services
{
    public class ShowService : IShowService
    {
        private readonly IEntityContext _entityContext;

        public ShowService(IEntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public async Task<ShowModel> GetShowAsync(string showId)
        {
            var shows = DB.Collection<ShowDocument>();
           var query = from d in shows.AsQueryable() where d.ID == showId select d;
            var result = await query.FirstOrDefaultAsync();
            return _entityContext.Mapper.Map<ShowModel>(result);
        }

        public async Task<IEnumerable<ShowModel>> GetShowsAsync()
        {
            var shows = DB.Collection<ShowDocument>();
            var query = from d in shows.AsQueryable() select d;
            var result = await query.ToListAsync();



         
            return _entityContext.Mapper.MapCollection<ShowModel, ShowDocument>(result);
        }

        class ShowEpsidoesDocument
        {
            public string ID { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }

            public List<EpisodeDocument> Episodes { get; set; }
        }
    }
}
