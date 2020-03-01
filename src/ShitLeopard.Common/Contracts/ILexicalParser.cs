using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShitLeopard.Common.Contracts
{
    public interface ILexicalParser
    {
        Task<string> ParseAsync();
    }
}
