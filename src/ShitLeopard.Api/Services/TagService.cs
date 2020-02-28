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
    public class TagService : BaseService, ITagService
    {
        public TagService(ILoggerFactory loggerFactory, Func<ShitLeopardContext> contextProvider, IMapper mapper) : base(loggerFactory, contextProvider, mapper)
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

            using (var context = ContextProvider())
            {
                var existing = await (context.Tags.FirstOrDefaultAsync(x =>
                   x.Category.Equals(tags.Category) &&
                   x.Name.Equals(tags.Name)));
                if (existing == null)
                {
                    var t = Mapper.Map<TagsModel, Tags>(tags);
                    t.Frequency = 1;
                    context.Tags.Add(t);
                    await context.SaveChangesAsync();
                }
                else
                {
                    existing.Frequency = existing.Frequency + 1;
                    context.Tags.Update(existing);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task RemoveAsync(TagsModel tags)
        {
            using (var context = ContextProvider())
            {
                var existing = await (context.Tags.Where(x =>
                    x.Category.Equals(tags.Category, StringComparison.OrdinalIgnoreCase) &&
                    x.Name.Equals(tags.Name, StringComparison.OrdinalIgnoreCase)).FirstOrDefaultAsync());
                if (existing != null)
                {
                    context.Tags.Remove(existing);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<IEnumerable<TagsModel>> GetMostPopularTagsAsync(string category, int count)
        {
            if (string.IsNullOrEmpty(category))
            {
                return Enumerable.Empty<TagsModel>();
            }
            using (var context = ContextProvider())
            {
                var query = from t in context.Tags select t;
                query = query.Where(x => x.Category.Equals(category));
                query = query.Where(x => !string.IsNullOrEmpty(x.Name));

                query = query.OrderByDescending(x => x.Frequency);
                return Mapper.MapCollection<TagsModel, Tags>(await (query.Take(count).ToListAsync()));
            }
        }

        public async Task<IEnumerable<TagsModel>> SearchAsync(string category, string name)
        {
            if (string.IsNullOrEmpty(category))
            {
                return Enumerable.Empty<TagsModel>();
            }

            using (var context = ContextProvider())
            {
                var query = from t in context.Tags select t;
                query = query.Where(x => x.Category.Equals(category));
                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(x => x.Name.StartsWith(name));
                }
                query = query.OrderByDescending(x => x.Frequency);
                return Mapper.MapCollection<TagsModel, Tags>(await (query.ToListAsync()));
            }
        }

        public async Task<IEnumerable<string>> SearchCategoriesAsync(string term = null)
        {
            using (var context = ContextProvider())
            {
                if (!string.IsNullOrEmpty(term))
                {
                    return await (context.Tags.Where(x =>
                        x.Category.StartsWith(term, StringComparison.OrdinalIgnoreCase)).Select(x => x.Category).Distinct().ToListAsync());
                }
                else
                {
                    return await (context.Tags.Select(x => x.Category).Distinct().ToListAsync());
                }
            }
        }
    }
}