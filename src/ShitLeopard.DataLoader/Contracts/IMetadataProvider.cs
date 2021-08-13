using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShitLeopard.DataLayer.Entities;
using ShitLeopard.DataLoader.Configuration;
using ShitLeopard.DataLoader.Models;

namespace ShitLeopard.DataLoader.Contracts
{
    public interface IMetadataProvider
    {
        public Task<IEnumerable<EpisodeMetadata>> GetMetadataAsync(ShowConfiguration showConfiguration);
    }
}
