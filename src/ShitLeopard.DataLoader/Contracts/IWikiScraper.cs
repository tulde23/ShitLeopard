using System.Collections.Generic;
using System.Threading.Tasks;
using ShitLeopard.DataLayer.Entities;

namespace ShitLeopard.DataLoader.Contracts
{
    public interface IWikiScraper
    {
        Task<IEnumerable<Episode>> GetEpisodesAsync();
    }
}