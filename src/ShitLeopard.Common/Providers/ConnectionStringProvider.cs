using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShitLeopard.Common.Contracts;
using System;

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
            _logger = logger;
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
        }

        public string GetConnectionString()
        {
            var cs = GetString("connectionString");

            return cs;
        }

        public string GetString(string key)
        {
            try
            {
                var cs = _configuration[key];

                var secretKey = _configuration[cs];

                try
                {
                    var k = _configuration[secretKey];
                    if(!string.IsNullOrEmpty(k))
                    {
                        secretKey = k;
                    }
                }
                catch { }
          

                
                _logger.LogWarning($"Mongo Connection {key}= {secretKey} ");
                return string.IsNullOrEmpty(secretKey) ? cs : secretKey;
            }
            catch(Exception ex)
            {
                _logger.LogWarning($"{key} does not exist.");
                return null;
            }
        }
    }
}