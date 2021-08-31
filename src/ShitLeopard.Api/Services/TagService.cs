using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShitLeopard.Api.Contracts;
using ShitLeopard.Api.Models;
using ShitLeopard.Common.Contracts;
using ShitLeopard.Common.Documents;

namespace ShitLeopard.Api.Services
{
    public class TagService : ITagService
    {
        private readonly IElasticSearchConnectionProvider _elasticSearchConnectionProvider;

        public TagService(IElasticSearchConnectionProvider elasticSearchConnectionProvider)
        {
            _elasticSearchConnectionProvider = elasticSearchConnectionProvider;
        }

        public Task<IEnumerable<TagsModel>> GetMostPopularTagsAsync(string category, int count)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveAsync(TagsModel tags)
        {
            throw new System.NotImplementedException();
        }

        public async Task SaveTagAsync(TagsModel tags)
        {
            var document = new TagsDocument();
            document.DocumentId = Guid.NewGuid().ToString();
            document.CreateTime = DateTime.UtcNow;
            document.UpdateTime = DateTime.UtcNow;
         
            await _elasticSearchConnectionProvider.Client.IndexAsync(document, b => b.Index("tags"));
      
        }

        public Task<IEnumerable<TagsModel>> SearchAsync(string category, string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<string>> SearchCategoriesAsync(string term = null)
        {
            throw new System.NotImplementedException();
        }
    }
}