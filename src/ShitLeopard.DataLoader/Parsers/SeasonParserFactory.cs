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

        public ISeasonParser GetParser(string key)
        {
            var parser = _index[key];
            return parser;
        }
    }
}