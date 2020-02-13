using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ShitLeopard.DataLayer.Entities;

namespace ShitLeopard.DataLoader.Contracts
{
    public interface ISeasonParser
    {
        /// <summary>
        /// Loads and parsers local files to produce season instances.
        /// </summary>
        /// <param name="directoryInfo">The directory information.</param>
        /// <returns></returns>
        Task<IEnumerable<Season>> GetSeasonsAsync(DirectoryInfo directoryInfo);
    }
}