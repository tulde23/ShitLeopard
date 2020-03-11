using System.Threading.Tasks;
using ShitLeopard.Common.Models;

namespace ShitLeopard.Common.Contracts
{
    public interface INaturalLanguageService
    {
        Task<ParsedSentence> ParseSentenceAsync(string sentence);
    }
}