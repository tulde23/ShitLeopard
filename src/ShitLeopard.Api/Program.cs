using System;
using System.Diagnostics;
using App.Metrics;
using App.Metrics.AspNetCore;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Events;
using ShitLeopard.Api;

namespace ShitLeopard
{
    public class Program
    {
        public static int Main(string[] args)
        {

            Activity.DefaultIdFormat = ActivityIdFormat.W3C;
            Activity.ForceDefaultIdFormat = true;

            Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Debug()
           .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
           .Enrich.FromLogContext()
           .WriteTo.Console()
           .CreateLogger();

            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseMetrics()
            .UseMetricsWebTracking(c=> { c.ApdexTrackingEnabled = true; c.ApdexTSeconds = 5; })
             .ConfigureMetricsWithDefaults(
                builder =>
                {
                    builder.Report.ToInfluxDb("http://192.168.86.32:8086", "shitleopard");
                })

            .UseServiceProviderFactory(new AutofacServiceProviderFactory(cb => AutoFacRegistrationModule.Build(cb)))
             .ConfigureLogging((context, builder) =>
             {
                
             })
            .ConfigureWebHostDefaults(webBuilder =>
                {
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