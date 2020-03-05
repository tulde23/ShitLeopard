using System.Threading.Tasks;

namespace ShitLeopard.Common.Contracts
{
    public interface INaturalLanguageService
    {
        Task<dynamic> ParseSentenceAsync(string sentence);
    }
}