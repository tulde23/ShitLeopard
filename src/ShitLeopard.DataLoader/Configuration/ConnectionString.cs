using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ShitLeopard.DataLoader.Configuration
{
    public class ConnectionString
    {
        public string Value { get; }
        public ConnectionString( IConfiguration configuration, Options options)
        {
            var cs = configuration["connectionString"];
            Value = configuration[cs];
        }

   
    }
}
