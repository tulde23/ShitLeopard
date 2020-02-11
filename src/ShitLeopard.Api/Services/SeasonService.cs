using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShitLeopard.Api.Contracts;
using ShitLeopard.Api.Models;
using ShitLeopard.DataLayer.Entities;

namespace ShitLeopard.Api.Services
{
    public class SeasonService : BaseService, ISeasonService
    {
        public SeasonService(ILoggerFactory loggerFactory, Func<ShitLeopardContext> contextProvider, IMapper mapper) : base(loggerFactory, contextProvider, mapper)
        {
        }

        public async Task<SeasonModel> GetSeasonAsync(long seasonId)
        {
            using (var context = ContextProvider())
            {
                return Mapper.Map<SeasonModel>(await context.Season.AsNoTracking().Include(x => x.Episode).ThenInclude(x => x.Script).FirstOrDefaultAsync(x => x.Id == seasonId));
            }
        }

        public async Task<IEnumerable<SeasonModel>> ListAsync()
        {
            using (var context = ContextProvider())
            {
                return Mapper.MapCollection<SeasonModel, Season>(await context.Season.AsNoTracking().ToListAsync());
            }
        }
    }
}