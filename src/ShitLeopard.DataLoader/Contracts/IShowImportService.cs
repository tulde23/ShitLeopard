using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShitLeopard.DataLoader.Configuration;

namespace ShitLeopard.DataLoader.Contracts
{
    public interface IShowImportService
    {
 
         Task ImportAsync(ShowConfiguration showConfiguration);
    }
}
