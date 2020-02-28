using System.Collections.Generic;
using System.Threading.Tasks;
using ShitLeopard.Api.Models;

namespace ShitLeopard.Api.Contracts
{
    public interface ITagService 
    {
        /// <summary>
        /// searches for  a tag by category and name.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        Task<IEnumerable<TagsModel>> SearchAsync(string category, string name);

        /// <summary>
        /// Searches the categories asynchronous.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <returns></returns>
        Task<IEnumerable<string>> SearchCategoriesAsync(string term = null);

        /// <summary>
        /// Gets the most popular tags asynchronous.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        Task<IEnumerable<TagsModel>> GetMostPopularTagsAsync(string category, int count);
        /// <summary>
        /// Adds a new tag.
        /// </summary>
        /// <param name="tags">The tags.</param>
        /// <returns></returns>
        Task SaveTagAsync(TagsModel tags);

        /// <summary>
        /// Removes the asynchronous.
        /// </summary>
        /// <param name="tags">The tags.</param>
        /// <returns></returns>
        Task RemoveAsync(TagsModel tags);
    }
}