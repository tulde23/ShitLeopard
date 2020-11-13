using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Entities;
using ShitLeopard.Api.Contracts;
using ShitLeopard.Api.Models;
using ShitLeopard.Common.Contracts;
using ShitLeopard.Common.Documents;

namespace ShitLeopard.Api.Services
{
    public class TagService : ITagService
    {
        private readonly IEntityContext _entityContext;

        public TagService(IEntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public async Task SaveTagAsync(TagsModel tags)
        {
            if (tags == null ||
                string.IsNullOrEmpty(tags.Name) ||
                tags.Name.Length <= 2)
            {
                return;
            }

            var existing = await FindByNameAndCategoryAsync(tags.Name, tags.Category);
            if (existing == null)
            {
                var t = _entityContext.Mapper.Map<TagsDocument>(tags);
                t.Frequency = 1;
                await t.SaveAsync();
            }
            else
            {
                existing.Frequency = existing.Frequency + 1;
                await DB.Update<TagsDocument>().Match(x => x.Eq(f => f.ID, existing.ID)).Modify(x => x.Frequency, existing.Frequency++).ExecuteAsync();
            }
        }

        public async Task RemoveAsync(TagsModel tags)
        {
            var existing = await FindByNameAndCategoryAsync(tags.Name, tags.Category);
            if (existing != null)
            {
                await DB.DeleteAsync<TagsDocument>(existing.ID);
            }
        }

        public async Task<IEnumerable<TagsModel>> GetMostPopularTagsAsync(string category, int count)
        {
            if (string.IsNullOrEmpty(category))
            {
                return Enumerable.Empty<TagsModel>();
            }

            var query = from t in DB.Queryable<TagsDocument>() select t;

            query = query.Where(x => x.Category.Equals(category));
            query = query.Where(x => !string.IsNullOrEmpty(x.Name));
            query = query.OrderByDescending(x => x.Frequency);

            var results = await (query.Take(count).ToListAsync());
            return _entityContext.Mapper.MapCollection<TagsModel, TagsDocument>(results);
        }

        public async Task<IEnumerable<TagsModel>> SearchAsync(string category, string name)
        {
            if (string.IsNullOrEmpty(category))
            {
                return Enumerable.Empty<TagsModel>();
            }

            var query = from t in DB.Queryable<TagsDocument>() select t;
            query = query.Where(x => x.Category.Equals(category));
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.StartsWith(name));
            }
            query = query.OrderByDescending(x => x.Frequency);

            return _entityContext.Mapper.MapCollection<TagsModel, TagsDocument>(await (query.ToListAsync()));
        }

        public async Task<IEnumerable<string>> SearchCategoriesAsync(string term = null)
        {
            if (!string.IsNullOrEmpty(term))
            {
                var results = await DB.Collection<TagsDocument>().Find(Builders<TagsDocument>.Filter.Text(term, new TextSearchOptions { CaseSensitive = true })).Project(x => x.Category).ToListAsync();
                return results;
            }
            else
            {
                var results = await DB.Collection<TagsDocument>().Distinct(x => x.Category, Builders<TagsDocument>.Filter.Text(term, new TextSearchOptions { CaseSensitive = true })).ToListAsync();
                return results;
            }
        }

        private async Task<TagsDocument> FindByNameAndCategoryAsync(string name, string category)
        {
            var query = from t in DB.Queryable<TagsDocument>()
                        where t.Name == name && t.Category == category
                        select t;
            return await query.FirstOrDefaultAsync();
        }
    }
}