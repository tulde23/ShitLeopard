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
        private readonly IIndex<string, ISeasonParser> _index;
        private readonly Options _options;

        public ConsoleService(ConsoleApplication consoleApplication,
            IIndex<string, ISeasonParser> index,
            Options options)
        {
            _consoleApplication = consoleApplication;
            _index = index;
            _options = options;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var service = "xdoc";
            var parser = _index[service];
            var seasons = await parser.GetSeasonsAsync(_options.DocumentDirectory);
            _consoleApplication.TokenSource.Cancel();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _consoleApplication.TokenSource.Cancel();
            return Task.CompletedTask;
        }
    }
}