using Microsoft.Extensions.Configuration;
using ShitLeopard.Common.Contracts;

namespace ShitLeopard.Common.Providers
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ConnectionStringProvider(IConfiguration configuration)
        {
            _configuration = configuration;
            var cs = _configuration["connectionString"];
            var secretKey = _configuration[cs];
            _connectionString = _configuration[secretKey];
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}