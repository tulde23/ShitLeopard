using System.Threading.Tasks;
using AutoMapper;
using ShitLeopard.Common.Contracts;
using ShitLeopard.Common.Documents;
using ShitLeopard.Common.Models;

namespace ShitLeopard.Api.Services
{
    public class RequestProfileService : IRequestProfileService
    {
        private readonly IElasticSearchConnectionProvider _elasticSearchConnectionProvider;
        private readonly IMapper _mapper;

        public RequestProfileService(IElasticSearchConnectionProvider elasticSearchConnectionProvider, IMapper mapper)
        {
            _elasticSearchConnectionProvider = elasticSearchConnectionProvider;
            _mapper = mapper;
        }

        public async Task SaveAsync(RequestProfileModel requestProfileModel)
        {
           var document =  _mapper.Map<RequestProfileDocument>(requestProfileModel);
            document.DocumentId = requestProfileModel.ID;
   
            await _elasticSearchConnectionProvider.Client.IndexAsync(document, b => b.Index("search_requests"));
        }

        public Task<PagedResult<SiteMetricsModel>> SearchAsync(RequestProfileSearchCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}