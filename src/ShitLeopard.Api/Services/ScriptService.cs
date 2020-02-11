using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShitLeopard.Api.Contracts;
using ShitLeopard.Api.Models;
using ShitLeopard.DataLayer.Entities;

namespace ShitLeopard.Api.Services
{
    public class ScriptService : BaseService, IScriptService
    {
        public ScriptService(ILoggerFactory loggerFactory, Func<ShitLeopardContext> contextProvider, IMapper mapper) : base(loggerFactory, contextProvider, mapper)
        {
        }

        public async Task<ScriptModel> GetScript(long scriptId)
        {
            using (var context = ContextProvider())
            {
                return Mapper.Map<ScriptModel>(await context.Script.AsNoTracking()
            .Include(x => x.Episode)
            .Include(x => x.ScriptLine)
            .ThenInclude(y => y.ScriptWord)
            .SingleOrDefaultAsync(x => x.Id == scriptId));
            }
        }

        public async Task<IEnumerable<ScriptLineModel>> GetScriptLines(long scriptId, bool? includeAll = null)
        {
            using (var context = ContextProvider())
            {
                return Mapper.MapCollection<ScriptLineModel, ScriptLine>(await context.ScriptLine.AsNoTracking().Where(x => x.ScriptId == scriptId).ToListAsync());
            }
        }

        public async Task<IEnumerable<ScriptWordModel>> GetScriptWords(long scriptLineId)
        {
            using (var context = ContextProvider())
            {
                return Mapper.MapCollection<ScriptWordModel, ScriptWord>(await context.ScriptWord.AsNoTracking().Where(x => x.ScriptLineId == scriptLineId).ToListAsync());
            }
        }
    }
}