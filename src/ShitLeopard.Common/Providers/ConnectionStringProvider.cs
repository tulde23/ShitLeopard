using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShitLeopard.Common.Contracts;

namespace ShitLeopard.Common.Providers
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly string _connectionString;
        private readonly ILogger<ConnectionStringProvider> _logger;

        public ConnectionStringProvider(IConfiguration configuration, IHostEnvironment hostEnvironment, ILogger<ConnectionStringProvider> logger)
        {
            try
            {
                _logger = logger;
                _configuration = configuration;
                _hostEnvironment = hostEnvironment;
                if (_hostEnvironment.IsProduction())
                {
                   _connectionString = _configuration["connectionString"];
                    _logger.LogWarning($"Production setting connection string " + _connectionString);

                }
                else
                {
                    var cs = _configuration["connectionString"];
                    var secretKey = _configuration[cs];
                    _connectionString = _configuration[secretKey];
                    _logger.LogWarning($"Non Production setting connection string " + _connectionString);


                }

                if (string.IsNullOrWhiteSpace(_connectionString))
                {
                    _connectionString = _configuration["connectionString"];
                    _logger.LogWarning($"Non Production setting connection string " + _connectionString);
                }
            }
            catch
            {
                _connectionString = _configuration["connectionString"];
                _logger.LogWarning($"Non Production setting connection string " + _connectionString);
            }

            
        }

        public string GetConnectionString()
        {
            Console.WriteLine(_connectionString);
            return _connectionString;
        }
    }
}