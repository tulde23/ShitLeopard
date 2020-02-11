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


//            var data = File.ReadAllText(@"C:\Logs\replay.events.json");
//            var jarray =JsonConvert.DeserializeObject<List< Rootobject>>(data);

//            var exclusions = new List<string>()
//            {
//"5","48"
//            };
//            var ids = jarray.Select(x => x.NgbId).Distinct().ToList();

//            var test = string.Join(",", ids.OrderBy(x => x));
//           int count =  jarray.RemoveAll(x => !exclusions.Contains(x.NgbId));

//           foreach( var item in jarray.GroupBy(x => x.NgbId))
//            {
//                Console.WriteLine($"{item.Key} -> ,  {item.Count()}");
//            }

//            Console.WriteLine(count);


//            File.WriteAllText("Failed-LL-5-48.json", JsonConvert.SerializeObject(jarray, Formatting.Indented));
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory(cb=>AutoFacRegistrationModule.Build(cb)))
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