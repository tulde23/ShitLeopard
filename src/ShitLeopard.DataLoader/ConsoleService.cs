using System.Threading;
using System.Threading.Tasks;
using Autofac.Features.Indexed;
using Microsoft.Extensions.Hosting;
using ShitLeopard.DataLoader.Contracts;

namespace ShitLeopard.DataLoader
{
    internal class ConsoleService : IHostedService
    {
        private readonly ConsoleApplication _consoleApplication;
        private readonly IIndex<string, IShowBulkDataLoader> _index;
        private readonly IShowBulkDataImporter _bulkDataImporter;
        private readonly IWikiScraper _wikiScraper;
        private readonly Options _options;

        public ConsoleService(ConsoleApplication consoleApplication,
            IIndex<string, IShowBulkDataLoader> index,
            IShowBulkDataImporter bulkDataImporter,
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
            var service = _index[_options.ShowName.ToLower()];
            await service.ImportAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _consoleApplication.TokenSource.Cancel();
            return Task.CompletedTask;
        }
    }
}