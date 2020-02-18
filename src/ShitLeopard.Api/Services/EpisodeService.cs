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
    public class EpisodeService : BaseService, IEpisodeService
    {
        public EpisodeService(ILoggerFactory loggerFactory, Func<ShitLeopardContext> contextProvider, IMapper mapper) : base(loggerFactory, contextProvider, mapper)
        {
        }

        public async Task<EpisodeModel> GetEpisode(long episodeId)
        {
            using (var context = ContextProvider())
            {


                return Mapper.Map<EpisodeModel>(await context.Episode.AsNoTracking()
                        .Include(x => x.Script)
                        .ThenInclude(x => x.ScriptLine)
                        .FirstOrDefaultAsync(x => x.Id == episodeId));
            }
        }

        public async Task<IEnumerable<EpisodeModel>> GetEpisodes()
        {
            using (var context = ContextProvider())
            {
                return Mapper.MapCollection<EpisodeModel, Episode>(await context.Episode.AsNoTracking().ToListAsync());
            }
        }
    }
}