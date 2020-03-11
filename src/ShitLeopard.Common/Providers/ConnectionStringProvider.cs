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
            Console.WriteLine(cs);
            return cs;
        }

        public string GetString(string key)
        {
            try
            {
                if (_hostEnvironment.IsProduction())
                {
                    var val = _configuration[key];
                    _logger.LogWarning($"Production setting {key}= {val}");
                    return val;
                }
                else
                {
                    var cs = _configuration[key];
                    var secretKey = _configuration[cs];
                    var val = _configuration[secretKey];

                    if (string.IsNullOrWhiteSpace(val))
                    {
                        val = cs;
                    }
                    _logger.LogWarning($"Non Production setting {key}= {val}");
                    return val;
                }
            }
            catch
            {
                var val = _configuration[key];
                _logger.LogWarning($"Non Production setting {key}= {val}");
                return val;
            }
        }
    }
}