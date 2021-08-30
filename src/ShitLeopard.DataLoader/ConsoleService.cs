using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ShitLeopard.DataLoader.Configuration;
using ShitLeopard.DataLoader.Contracts;
using ShitLeopard.DataLoader.Models;

namespace ShitLeopard.DataLoader
{
    internal class ConsoleService : IHostedService
    {
        private readonly ConsoleApplication _consoleApplication;
        private readonly IShowImportService _showImportService;
        private readonly IElasticDocumentIndexService _bulkDataImporter;
        private readonly IConsoleLogger _consoleLogger;
        private readonly Options _options;

        public ConsoleService(ConsoleApplication consoleApplication,
           IShowImportService showImportService,
            IElasticDocumentIndexService bulkDataImporter,
            IMetadataProvider wikiScraper,
            IConsoleLogger consoleLogger,
            Options options)
        {
            _consoleApplication = consoleApplication;
            _showImportService = showImportService;
            _bulkDataImporter = bulkDataImporter;
            _consoleLogger = consoleLogger;
            _options = options;
        }

        /// <summary>
        /// Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
        public async Task StartAsync(CancellationToken cancellationToken)

        {
            _consoleLogger.Write($"Starting at {DateTime.Now}");
            //initialize and resources required by the bulk data importer
            await _bulkDataImporter.InitAsync();
            if (_options.DropCollections)
            {
                await _bulkDataImporter.DropDocumentsAsync();
            }

            ConcurrentQueue<Show> shows = new ConcurrentQueue<Show>();
            if (_options.ShowName.Equals("all", System.StringComparison.OrdinalIgnoreCase))
            {
                var dir = new DirectoryInfo(_options.DataDirectory);
                foreach (var configFile in dir.GetFiles("*.json"))
                {
                    var config = await LoadShowConfigurationAsync(configFile.DirectoryName, Path.GetFileNameWithoutExtension(configFile.FullName));
                    var show = await _showImportService.ImportAsync(config);
                    if (show != null)
                    {
                        shows.Enqueue(show);
                    }
                }
            }
            else
            {
                var config = await LoadShowConfigurationAsync(_options.DataDirectory, _options.ShowName);
                var show = await _showImportService.ImportAsync(config);
                if (show != null)
                {
                    shows.Enqueue(show);
                }
            }

            if (!shows.IsEmpty)
            {
              var result =   await _bulkDataImporter.BulkIndexAsync(shows);
                _consoleLogger.Write($"Indexed {result.showsIndexed} shows.");
                _consoleLogger.Write($"Indexed {result.seasonsIndexed} seasons.");
                _consoleLogger.Write($"Indexed {result.episodesIndex} episodes.");
 
            }
            _consoleApplication.TokenSource.Cancel();
        }

        private async Task<ShowConfiguration> LoadShowConfigurationAsync(string directory, string name)
        {
            var json = await File.ReadAllTextAsync(Path.Combine(directory, $"{name}.json"));
            var config = JsonConvert.DeserializeObject<ShowConfiguration>(json);
            config.RootFolder = directory;
            config.FileName = name;
            return config;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _consoleApplication.TokenSource.Cancel();
            return Task.CompletedTask;
        }
    }
}