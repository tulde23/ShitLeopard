using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Entities;
using ShitLeopard.Common.Contracts;
using ShitLeopard.Common.Documents;
using ShitLeopard.Common.Models;

namespace ShitLeopard.Api.Services
{
    public class RequestProfileService : IRequestProfileService
    {
        private readonly IEntityContext _entityContext;

        public RequestProfileService(IEntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public async Task SaveAsync(RequestProfileModel requestProfileModel)
        {
            if (requestProfileModel.Ipaddress != "::1")
            {
                var entity = _entityContext.Mapper.Map<RequestProfileDocument>(requestProfileModel);
                await entity.SaveAsync();
            }
        }

        public async Task<PagedResult<SiteMetricsModel>> SearchAsync(RequestProfileSearchCommand command)
        {
            var query = from p in DB.Queryable<RequestProfileDocument>() where p.Ipaddress != "::1" select p;
            query = query.OrderByDescending(x => x.LastAccessTime).Skip(command.PageNumber * command.PageSize).Take(command.PageSize);
            var count = query.Count();
            var results = await query.ToListAsync();

            var data = _entityContext.Mapper.MapCollection<SiteMetricsModel, RequestProfileDocument>(results);

            return new PagedResult<SiteMetricsModel>
            {
                Count = count,
                Result = data.ToList()
            };
        }
    }
}