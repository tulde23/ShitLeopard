using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShitLeopard.DataLoader.Contracts
{
    public interface IShowBulkDataLoader
    {
 

        Task ImportAsync();
    }
}
