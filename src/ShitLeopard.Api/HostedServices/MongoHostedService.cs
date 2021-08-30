using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using MongoDB.Entities;
using ShitLeopard.Common.Contracts;

namespace ShitLeopard.Api.HostedServices
{
    public class MongoHostedService : IHostedService
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public MongoHostedService(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var connectionString = _connectionStringProvider.GetConnectionString();
            var dbName = _connectionStringProvider.GetString("database");
            await DB.InitAsync(dbName ?? "ShitLeopard", MongoClientSettings.FromConnectionString(connectionString));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}