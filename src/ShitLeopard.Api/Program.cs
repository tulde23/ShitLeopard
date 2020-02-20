using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ShitLeopard.Api;

namespace ShitLeopard
{
    public class Program
    {
        public static void Main(string[] args)
        {
           CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                       .UseEnvironment("development")
            .UseServiceProviderFactory(new AutofacServiceProviderFactory(cb=>AutoFacRegistrationModule.Build(cb)))
                .ConfigureWebHostDefaults(webBuilder =>
                {

                    webBuilder.UseMetricsEndpoints();
                    webBuilder.UseMetricsWebTracking();
              
                 
                    webBuilder.UseStartup<Startup>();
                });
    }



    public class Rootobject
    {
        public string NgbId { get; set; }
        public string EnvironmentName { get; set; }
        public int? TeamId { get; set; }
        public DateTime? PostedOn { get; set; }
        public int? UserId { get; set; }
        public string PortalName { get; set; }
        public string InstanceKey { get; set; }
        public int? PortalId { get; set; }
        public int? ProgramId { get; set; }
        public long? CommandId { get; set; }
        public int? PlayerId { get; set; }

        public int? VolunteerId { get; set; }

        public long? FamilyId { get; set; }
        public int? DivisionId { get; set; }
        public string ProgramName { get; set; }
    }

}