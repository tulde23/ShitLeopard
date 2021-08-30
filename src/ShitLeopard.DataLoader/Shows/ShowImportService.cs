using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Features.Indexed;
using Newtonsoft.Json;
using ShitLeopard.DataLoader.Configuration;
using ShitLeopard.DataLoader.Contracts;
using ShitLeopard.DataLoader.Models;

namespace ShitLeopard.DataLoader.Shows
{
    public class ShowImportService : IShowImportService
    {
        private readonly IIndex<string, IMetadataProvider> _metadataProvider;
        private readonly ISeasonParserFactory _seasonParserFactory;
        private readonly IElasticDocumentIndexService _showBulkDataImporter;
        private readonly IConsoleLogger _consoleLogger;

        public ShowImportService(ConsoleApplication consoleApplication, Options options,
            ISeasonParserFactory seasonParserFactory,
            IIndex<string, IMetadataProvider> metadataProvider, IElasticDocumentIndexService showBulkDataImporter, IConsoleLogger consoleLogger)
        {
            _seasonParserFactory = seasonParserFactory;
            _metadataProvider = metadataProvider;
            _showBulkDataImporter = showBulkDataImporter;
            _consoleLogger = consoleLogger;
            ConsoleApplication = consoleApplication;
        }

        public ConsoleApplication ConsoleApplication { get; }
        public Options Options { get; }

        public async Task<Show> ImportAsync(ShowConfiguration showConfiguration)
        {
            var show = new Show(showConfiguration);
            _consoleLogger.Write("Downloading Episode Metadata For " + showConfiguration.ShowName, ConsoleColor.Magenta);

            var exists = TryGetService(showConfiguration.Metadata.Provider, out var service);
            if (!exists)
            {
                _consoleLogger.Write($"no metadata service  exists with key {showConfiguration.Metadata.Provider}.  Exiting... ", ConsoleColor.Red);
                return null;
            }
            var episodes = await service.GetMetadataAsync(showConfiguration);
            _consoleLogger.Write("Downloaded " + episodes.Count() + " episode metadata");
            exists = _seasonParserFactory.TryGetParser(showConfiguration.ClosedCaptionsParser, out var parser);
            if (!exists)
            {
                _consoleLogger.Write($"no closed caption parser  exists with key {showConfiguration.ClosedCaptionsParser} .  Exiting...", ConsoleColor.Red);
                return null;
            }

            IEnumerable<Season> seasons = null;
            if (System.IO.File.Exists(showConfiguration.GetCacheFileName()))
            {
                var text = System.IO.File.ReadAllText(showConfiguration.GetCacheFileName());
                seasons = JsonConvert.DeserializeObject<List<Season>>(text);
            }
            else
            {
                seasons = await parser.GetSeasonsAsync(showConfiguration);
                var json = JsonConvert.SerializeObject(seasons, Formatting.Indented);
                System.IO.File.WriteAllText(showConfiguration.GetCacheFileName(), json);
            }

            var allEpisodes = seasons.SelectMany(x => x.Episode.Select(y => new { Episode = y, Season = x })).ToList();
            foreach (var e in episodes)
            {
                var match = allEpisodes.FirstOrDefault(x => x.Episode.Id == e.Id);
                if (match != null)
                {
                    match.Episode.Title = e.Title;
                    match.Episode.Synopsis = e.Synopsis;
                    match.Episode.OffsetId = e.OffsetId;
                    match.Episode.Season = match.Season;
                    match.Episode.Show = show;
                }
            }
            show.Seasons = new List<Season>(seasons);
            return show;

            //var sw = new Stopwatch();
            //sw.Start();
            //_consoleLogger.Write($"Importing {seasons.Count()} seasons and {seasons.SelectMany(x => x.Episode).Count()} episodes for '{showConfiguration.ShowName}'", ConsoleColor.Cyan);
            //await _showBulkDataImporter.IndexShowsAsync(seasons);
            //sw.Stop();
            //_consoleLogger.Write($"Imported documents in {sw.Elapsed.TotalSeconds} seconds.", ConsoleColor.Cyan);
            //ConsoleApplication.TokenSource.Cancel();
        }

        private bool TryGetService(string key, out IMetadataProvider provider)
        {
            try
            {
                provider = _metadataProvider[key];
                return true;
            }
            catch
            {
                provider = null;
                return false;
            }
        }
    }
}