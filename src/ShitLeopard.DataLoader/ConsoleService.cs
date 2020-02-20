using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Autofac.Features.Indexed;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ShitLeopard.DataLayer.Entities;
using ShitLeopard.DataLoader.Contracts;

namespace ShitLeopard.DataLoader
{
    internal class ConsoleService : IHostedService
    {
        private readonly ConsoleApplication _consoleApplication;
        private readonly IIndex<string, ISeasonParser> _index;
        private readonly IBulkDataImporter _bulkDataImporter;
        private readonly IWikiScraper _wikiScraper;
        private readonly Options _options;

        public ConsoleService(ConsoleApplication consoleApplication,
            IIndex<string, ISeasonParser> index,
            IBulkDataImporter bulkDataImporter,
            IWikiScraper wikiScraper,
            Options options)
        {
            _consoleApplication = consoleApplication;
            _index = index;
            _bulkDataImporter = bulkDataImporter;
            _wikiScraper = wikiScraper;
            _options = options;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var episodes = await _wikiScraper.GetEpisodesAsync();
            await _bulkDataImporter.UpdateEpisodes(episodes);
        }

        private async Task RunImport()
        {
            var service = "xdoc";
            var parser = _index[service];

            IEnumerable<Season> seasons = null;
            if (System.IO.File.Exists("Data.json"))
            {
                var text = System.IO.File.ReadAllText("Data.json");
                seasons = JsonConvert.DeserializeObject<List<Season>>(text);
            }
            else
            {
                seasons = await parser.GetSeasonsAsync(new DirectoryInfo(_options.ImportDirectory));
            }
            var json = JsonConvert.SerializeObject(seasons, Formatting.Indented);
            System.IO.File.WriteAllText("Data.json", json);
            await _bulkDataImporter.ImportAsync(seasons);
            _consoleApplication.TokenSource.Cancel();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _consoleApplication.TokenSource.Cancel();
            return Task.CompletedTask;
        }
    }
}