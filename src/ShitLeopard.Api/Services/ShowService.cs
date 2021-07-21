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

        public async Task<IEnumerable<ShowModel>> GetShowsAsync()
        {
            var query = from d in DB.Queryable<ShowDocument>() select d;
            var result = await query.ToListAsync();
            return _entityContext.Mapper.MapCollection<ShowModel, ShowDocument>(result);
        }
    }
}
