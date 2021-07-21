using System;
using System.Collections.Generic;
using System.Text;

namespace ShitLeopard.DataLoader.Contracts
{
    public interface ISeasonParserFactory
    {
        ISeasonParser GetParser(string key);
    }
}
