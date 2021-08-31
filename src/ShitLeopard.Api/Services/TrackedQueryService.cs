using System;
using System.Threading.Tasks;
using AutoMapper;
using ShitLeopard.Common.Contracts;
using ShitLeopard.Common.Documents;
using ShitLeopard.Common.Models;
using System.Linq;
using System.Collections.Generic;

namespace ShitLeopard.Api.Services
{
    public class TrackedQueryService : ITrackedQueryService
    {
        private readonly IElasticSearchConnectionProvider _elasticSearchConnectionProvider;
        private readonly IMapper _mapper;

        public TrackedQueryService(IElasticSearchConnectionProvider elasticSearchConnectionProvider, IMapper mapper)
        {
            _elasticSearchConnectionProvider = elasticSearchConnectionProvider;
            _mapper = mapper;
        }

        public async Task SaveAsync(TrackedQueryModel requestProfileModel)
        {
           var document =  _mapper.Map<TrackedQueryDocument>(requestProfileModel);

            document.DocumentId = Guid.NewGuid().ToString();
            document.CreateTime = DateTime.UtcNow;
            document.UpdateTime = DateTime.UtcNow;
            await _elasticSearchConnectionProvider.Client.IndexAsync(document, b => b.Index("tracked_queries"));
        }

        public async Task<PagedResult<SiteMetricsModel>> SearchAsync(TrackedQuerySearchCommand command)
        {
            var searchResults = await this._elasticSearchConnectionProvider.Client.SearchAsync<TrackedQueryDocument>(
     s => s
             .Size(command.PageSize)
             .Sort(s=>s.Descending(x=>x.CreateTime))
             .Query(q => q
                 .MatchAll()));


            var results =  _mapper.Map<List<SiteMetricsModel>>(searchResults.Documents);
            return new PagedResult<SiteMetricsModel>()
            {
                Count = (int)searchResults.Total,
                Result = results
            };
                 
        }
    }
}