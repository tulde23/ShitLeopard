using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShitLeopard.Common.Contracts;
using ShitLeopard.Common.Models;
using System.Linq;
using ShitLeopard.Common.Documents;

namespace ShitLeopard.Api.Services
{
    public class ShowService : IShowService
    {
        private readonly IElasticSearchConnectionProvider _elasticSearchConnectionProvider;

        public ShowService( IElasticSearchConnectionProvider elasticSearchConnectionProvider)
        {
            _elasticSearchConnectionProvider = elasticSearchConnectionProvider;
        }

        public Task<ShowModel> GetShowAsync(string showId)
        {
            throw new NotImplementedException();
        }

        public async  Task<IEnumerable<ShowModel>> GetShowsAsync()
        {

            var results = await _elasticSearchConnectionProvider.Client.SearchAsync<ShowDocument>(
                    s => s.Query(q => q.MatchAll())
                ) ;

            return results.Documents.Select(x => new ShowModel
            {
                 Description = x.Description,
                  Id = x.DocumentId,
                   Title = x.Title
            });
        }
    }
}