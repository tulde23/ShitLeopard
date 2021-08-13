using System;
using System.Collections.Generic;
using System.Text;

namespace ShitLeopard.DataLoader.Contracts
{
    public interface ISeasonParserFactory
    {
        bool TryGetParser(string key, out ISeasonParser seasonParser);
    }
}
