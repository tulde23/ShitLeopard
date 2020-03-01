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

        public async Task UpdateScriptLineAsync(ScriptLineModel model)
        {
            using (var context = ContextProvider())
            {
                var item = await context.ScriptLine.FirstOrDefaultAsync(x => x.Id == model.Id);

                if (item != null)
                {
                    var existingLength = item.Body.Length - model.Body.Length;
                    if( existingLength < 0)
                    {
                        existingLength = existingLength * -1;
                    }
                    if( !string.IsNullOrEmpty(model.Body) &&  existingLength <= 10)
                    {
                        item.Body = model.Body;
                    }
                    item.CharacterId = model.CharacterId;
                    context.Update(item);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}