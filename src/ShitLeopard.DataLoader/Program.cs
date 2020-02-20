using System;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using CommandLine;
using CommandLine.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShitLeopard.DataLayer.Entities;

namespace ShitLeopard.DataLoader
{
    public class Program
    {
        private static ConsoleApplication consoleApplication = new ConsoleApplication();
     
        private static async Task Main(string[] args)
        {
            Title.Write();
            Options options = null;
            var result = Parser.Default.ParseArguments<Options>(args)
             .WithParsed((e) => options = e)

             .WithNotParsed((e) =>
             {
             });

            if (result.Tag == ParserResultType.NotParsed)
            {
                HelpText.AutoBuild(result);
                return;
            }

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "production";


            await Host.CreateDefaultBuilder(args)
                .UseEnvironment(env)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory(cb => AutoFacRegistrationModule.Build(cb)))
            .ConfigureServices((hostContext, services) =>
             {
                 services.AddSingleton(options);
                 services.AddSingleton(consoleApplication);
                 services.AddTransient<ShitLeopardContext>();
                 services.AddHostedService<ConsoleService>();
             })
             .Build().RunAsync(consoleApplication.TokenSource.Token);
        }
    }
}