using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ShitLeopard.DataLoader.Configuration;
using ShitLeopard.DataLoader.Contracts;

namespace ShitLeopard.DataLoader
{
    internal class ConsoleService : IHostedService
    {
        private readonly ConsoleApplication _consoleApplication;
        private readonly IShowImportService _showImportService;
        private readonly IShowBulkDataImporter _bulkDataImporter;
        private readonly IConsoleLogger _consoleLogger;
        private readonly Options _options;

        public ConsoleService(ConsoleApplication consoleApplication,
           IShowImportService showImportService,
            IShowBulkDataImporter bulkDataImporter,
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

        public async Task StartAsync(CancellationToken cancellationToken)

        {
            _consoleLogger.Write($"Starting at {DateTime.Now}");
            await _bulkDataImporter.InitAsync();
            if (_options.DropCollections)
            {
          
                await _bulkDataImporter.DropCollectionsAsync();
            }

            if (_options.ShowName.Equals("all", System.StringComparison.OrdinalIgnoreCase))
            {
                var dir = new DirectoryInfo(_options.DataDirectory);
                foreach (var configFile in dir.GetFiles("*.json"))
                {
                    var config = await LoadShowConfigurationAsync(configFile.DirectoryName, Path.GetFileNameWithoutExtension(configFile.FullName));
                    await _showImportService.ImportAsync(config);
                }
            }
            else
            {
                var config = await LoadShowConfigurationAsync(_options.DataDirectory, _options.ShowName);
                await _showImportService.ImportAsync(config);
            }
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