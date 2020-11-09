using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using ShitLeopard.Api.Contracts;
using ShitLeopard.Api.Models;
using ShitLeopard.Common.Contracts;
using ShitLeopard.Common.Documents;

namespace ShitLeopard.Api.Services
{
    public class TagService : BaseService, ITagService
    {
        public TagService(ILoggerFactory loggerFactory, IMongoProvider contextProvider, IMapper mapper) : base(loggerFactory, contextProvider, mapper)
        {
        }

        public async Task SaveTagAsync(TagsModel tags)
        {
            if (tags == null ||
                string.IsNullOrEmpty(tags.Name) ||
                tags.Name.Length <= 2)
            {
                return;
            }

            var collection = ContextProvider.GetTagsCollection();
            var query = from t in collection.AsQueryable() where t.Category.Equals(tags.Category) && t.Name.Equals(tags.Name) select t;

            var existing = await query.FirstOrDefaultAsync();
            if (existing == null)
            {
                var t = Mapper.Map<TagsModel, TagsDocument>(tags);
                t.Frequency = 1;
                await collection.InsertOneAsync(t);
            }
            else
            {
                existing.Frequency = existing.Frequency + 1;
                await collection.UpdateOneAsync(x => x.Id == existing.Id, Builders<TagsDocument>.Update.Set(x => x.Frequency, existing.Frequency));
            }
        }

        public async Task RemoveAsync(TagsModel tags)
        {
            var collection = ContextProvider.GetTagsCollection();
            var query = from t in collection.AsQueryable() select t;

            query = query.Where(x => x.Category.Equals(tags.Category));
            query = query.Where(x => x.Name.Equals( tags.Name));
            var existing = await query.FirstOrDefaultAsync();
            if (existing != null)
            {
                await collection.DeleteOneAsync(x => x.Id == existing.Id);
            }
        }

        public async Task<IEnumerable<TagsModel>> GetMostPopularTagsAsync(string category, int count)
        {
            if (string.IsNullOrEmpty(category))
            {
                return Enumerable.Empty<TagsModel>();
            }
            var collection = ContextProvider.GetTagsCollection();

            var query = from t in collection.AsQueryable() select t;

            query = query.Where(x => x.Category.Equals(category));
            query = query.Where(x => !string.IsNullOrEmpty(x.Name));

            query = query.OrderByDescending(x => x.Frequency);
            return Mapper.MapCollection<TagsModel, TagsDocument>(await (query.Take(count).ToListAsync()));
        }

        public async Task<IEnumerable<TagsModel>> SearchAsync(string category, string name)
        {
            if (string.IsNullOrEmpty(category))
            {
                return Enumerable.Empty<TagsModel>();
            }
            var collection = ContextProvider.GetTagsCollection();

            var query = from t in collection.AsQueryable() select t;
            query = query.Where(x => x.Category.Equals(category));
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.StartsWith(name));
            }
            query = query.OrderByDescending(x => x.Frequency);

            return Mapper.MapCollection<TagsModel, TagsDocument>(await (query.ToListAsync()));
        }

        public async Task<IEnumerable<string>> SearchCategoriesAsync(string term = null)
        {
            var collection = ContextProvider.GetTagsCollection();

            if (!string.IsNullOrEmpty(term))
            {
                var results = await collection.Find(Builders<TagsDocument>.Filter.Text(term, new TextSearchOptions { CaseSensitive = true })).Project(x => x.Category).ToListAsync();
                return results;
            }
            else
            {
                var results = await collection.Distinct(x => x.Category, Builders<TagsDocument>.Filter.Text(term, new TextSearchOptions { CaseSensitive = true })).ToListAsync();
                return results;
            }
        }
    }
}