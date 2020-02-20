using System.Collections.Generic;
using System.Threading.Tasks;
using ShitLeopard.DataLayer.Entities;

namespace ShitLeopard.DataLoader.Contracts
{
    public interface IBulkDataImporter 
    {
        /// <summary>
        /// Imports the asynchronous.
        /// </summary>
        /// <param name="seasons">The seasons.</param>
        /// <returns></returns>
        Task<int> ImportAsync(IEnumerable<Season> seasons);


        /// <summary>
        /// Updates the episodes.
        /// </summary>
        /// <param name="episodes">The episodes.</param>
        /// <returns></returns>
        Task<int> UpdateEpisodes(IEnumerable<Episode> episodes);
    }
}