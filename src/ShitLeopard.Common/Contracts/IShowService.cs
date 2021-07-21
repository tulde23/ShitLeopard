using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShitLeopard.Common.Models;

namespace ShitLeopard.Common.Contracts
{
    public interface IShowService
    {
        Task<IEnumerable<ShowModel>> GetShowsAsync();
    }
}
