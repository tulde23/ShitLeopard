using System.Collections.Generic;
using System.Threading.Tasks;
using ShitLeopard.DataLoader.Models;
using ShitLeopard.Common.Documents;
using ShitLeopard.DataLoader.Models;

namespace ShitLeopard.DataLoader.Contracts
{
    public interface IElasticDocumentIndexService
    {
        /// <summary>
        /// Initializes the asynchronous.
        /// </summary>
        /// <returns></returns>
        Task InitAsync();
        /// <summary>
        /// Recycles the indexes.
        /// </summary>
        /// <returns></returns>
        Task DropDocumentsAsync();


        Task<(int showsIndexed, int seasonsIndexed, int episodesIndex)> BulkIndexAsync(IEnumerable<Show> shows);

    }
}