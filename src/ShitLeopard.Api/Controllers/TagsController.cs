using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShitLeopard.Api.Contracts;
using ShitLeopard.Api.Models;

namespace ShitLeopard.Api.Controllers
{
    public class TagsController : ServiceController<ITagService>
    {
        public TagsController(ILoggerFactory loggerFactory, ITagService service) : base(loggerFactory, service)
        {
        }

        /// <summary>
        /// Searches the specified category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        public async Task<IEnumerable<TagsModel>> Search([FromQuery] string category, [FromQuery] string name)
        {
            return await Service.SearchAsync(category, name);
        }

        [HttpGet("Popular/{category}/{count}")]
        [Produces("application/json")]
        public async Task<IEnumerable<TagsModel>> GetMostPopularTags([FromRoute] string category, [FromRoute] int count)
        {
            return await Service.GetMostPopularTagsAsync(category,count);
        }

        /// <summary>
        /// Adds the specified tags.
        /// </summary>
        /// <param name="tags">The tags.</param>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task Add([FromBody] TagsModel tags)
        {
            await Service.SaveTagAsync(tags);
        }

        /// <summary>
        /// Deletes the specified tags.
        /// </summary>
        /// <param name="tags">The tags.</param>
        [HttpPost("Remove")]
        [Consumes("application/json")]
        public async Task Delete([FromBody] TagsModel tags)
        {
            await Service.RemoveAsync(tags);
        }
    }
}