using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShitLeopard.DataLoader.Configuration;
using ShitLeopard.DataLoader.Models;

namespace ShitLeopard.DataLoader.Contracts
{
    /// <summary>
    /// Imports a show and all it's data.  This consists of the show identity, the season(s) and the episode(s).
    /// </summary>
    public interface IShowImportService
    {
 
         Task<Show> ImportAsync(ShowConfiguration showConfiguration);
    }
}
