using System;
using System.Threading.Tasks;
using AutoMapper;
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
    }
}