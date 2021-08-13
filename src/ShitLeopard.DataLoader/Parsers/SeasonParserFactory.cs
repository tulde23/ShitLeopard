using Autofac.Features.Indexed;
using ShitLeopard.DataLoader.Contracts;

namespace ShitLeopard.DataLoader.Parsers
{
    internal class SeasonParserFactory : ISeasonParserFactory
    {
        private readonly IIndex<string, ISeasonParser> _index;

        public SeasonParserFactory(IIndex<string, ISeasonParser> index)
        {
            _index = index;
        }

        public bool TryGetParser(string key, out ISeasonParser seasonParser)
        {
            try
            {
                seasonParser = _index[key];
                return true;
            }
            catch
            {
                seasonParser = null;
                return false;
            }
          
          
        }
    }
}