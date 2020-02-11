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

        public async Task AddAsync(TagsModel tags)
        {
            using (var context = ContextProvider())
            {
                var existing = await (context.Tags.FirstOrDefaultAsync(x =>
                   x.Category.Equals(tags.Category, StringComparison.OrdinalIgnoreCase) &&
                   x.Name.Equals(tags.Name, StringComparison.OrdinalIgnoreCase)));
                if (existing == null)
                {
                    context.Tags.Add(Mapper.Map<TagsModel, Tags>(tags));
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

        public async Task<IEnumerable<string>> SearchAsync(string category, string name)
        {
            using (var context = ContextProvider())
            {
                return await (context.Tags.Where(x =>
                    x.Category.Equals(category, StringComparison.OrdinalIgnoreCase) &&
                    x.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase)).Select(x => x.Name).ToListAsync());
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