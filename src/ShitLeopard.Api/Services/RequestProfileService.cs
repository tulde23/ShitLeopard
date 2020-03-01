using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShitLeopard.Common.Contracts;
using ShitLeopard.Common.Models;
using ShitLeopard.DataLayer.Entities;

namespace ShitLeopard.Api.Services
{
    public class RequestProfileService : BaseService, IRequestProfileService
    {
        public RequestProfileService(ILoggerFactory loggerFactory, Func<ShitLeopardContext> contextProvider, IMapper mapper) : base(loggerFactory, contextProvider, mapper)
        {
        }

        public async Task SaveAsync(RequestProfileModel requestProfileModel)
        {
            using (var context = ContextProvider())
            {
                context.Add(Mapper.Map<RequestProfile>(requestProfileModel));
                await context.SaveChangesAsync();
            }
        }

        public async Task<PagedResult<SiteMetricsModel>> SearchAsync(RequestProfileSearchCommand command)
        {
            using (var context = ContextProvider())
            {
                var query = from p in context.RequestProfile select p;
                query = query.OrderByDescending(x => x.LastAccessTime);
                var count = query.Count();

                var result = await query.Skip(command.PageNumber * command.PageSize).Take(command.PageSize).ToListAsync();

                var data = Mapper.MapCollection<SiteMetricsModel, RequestProfile>(await query.ToListAsync());

                return new PagedResult<SiteMetricsModel>
                {
                    Count = count,
                    Result = data.ToList()
                };
            }
        }
    }
}