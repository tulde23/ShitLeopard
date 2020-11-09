using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using ShitLeopard.Api.Contracts;
using ShitLeopard.Api.Models;
using ShitLeopard.Common.Contracts;
using ShitLeopard.Common.Documents;

namespace ShitLeopard.Api.Services
{
    public class ScriptService : BaseService, IScriptService
    {
        public ScriptService(ILoggerFactory loggerFactory, IMongoProvider contextProvider, IMapper mapper) : base(loggerFactory, contextProvider, mapper)
        {
        }

        public async Task<ScriptModel> GetScriptAsync(long episodeId)
        {
            var collection = ContextProvider.GetLinesCollection();
            var documents = await collection.Find(Builders<LineDocument>.Filter.Eq(x => x.EpisodeId, episodeId)).ToListAsync();

            var sb = new StringBuilder();
            foreach (var item in documents)
            {
                sb.AppendLine(item.Body);
            }
            return new ScriptModel
            {
                Body = sb.ToString(),
                EpisodeId = episodeId
            };
        }

        public async Task<IEnumerable<ScriptWordModel>> GetScriptWordsForLineAsync(long scriptLineId)
        {
            var collection = ContextProvider.GetWordsCollection();

            var results = await collection.Find(Builders<WordDocument>.Filter.Eq(x => x.ScriptLineId, scriptLineId)).ToListAsync();

            return Mapper.MapCollection<ScriptWordModel, WordDocument>(results);
        }

        public async Task<IEnumerable<ScriptLineModel>> SearchScriptLinesAsync(string pattern)
        {
            var collection = ContextProvider.GetLinesCollection();
            var filter = Builders<LineDocument>.Filter.Empty;
            if (!string.IsNullOrEmpty(pattern))
            {
                filter = Builders<LineDocument>.Filter.Text(pattern, new TextSearchOptions { CaseSensitive = false });
            }
            var results = await collection.Find(filter).ToListAsync();
            return Mapper.MapCollection<ScriptLineModel, LineDocument>(results);
        }

        public async Task UpdateScriptLineAsync(ScriptLineModel model)
        {
            var collection = ContextProvider.GetLinesCollection();

            var result = await collection.FindOneAndUpdateAsync(s => s.Id == model.Id,
                 Builders<LineDocument>.Update
                    .Set(x => x.Body, model.Body));
        }
    }
}