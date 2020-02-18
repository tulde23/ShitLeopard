using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using DslParser.Parsing;
using DslParser.Parsing.Tokenizers.MoreEfficient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ShitLeopard.DataLayer.Entities;

namespace ShitLeopard.DataLoader
{
    public class Program
    {
        private static ConsoleApplication consoleApplication = new ConsoleApplication();
        private const string _defaultPath = @"C:\Development\Spotify.Playlister\src\Spotify.Playlister\bin\Debug\netcoreapp2.2\TPBoys.json";

        private static async Task Main(string[] args)
        {
            Console.WriteLine("Starting....");
            var options = new Options(args);


            string query = @"MATCH app = 'MyTestApp'
AND ex IN ('System.NullReferenceException', 'System.FormatException')
BETWEEN 2016-01-01 00:00:00 AND 2016-02-01 00:00:00
LIMIT 100";
            var parser = new Parser();
            var tokenizer = new PrecedenceBasedRegexTokenizer();
            var tokenSequence = tokenizer.Tokenize(query).ToList();
            foreach (var token in tokenSequence)
                Console.WriteLine(string.Format("TokenType: {0}, Value: {1}", token.TokenType, token.Value));

            var dataRepresentation = parser.Parse(tokenSequence);
            Console.WriteLine("");
            Console.WriteLine("Data Representation (serialized to JSON)");
            Console.WriteLine(JsonConvert.SerializeObject(dataRepresentation, Formatting.Indented));
            //XmlClosedCaptionParser.Parse(@"C:\Development\ShitLeopard\ClosedCaptions\s1");

            //var json = DataParser.GetDocument(_defaultPath);
            //var seasons = DataParser.GetSeasons(json);
            //using (var db = new ShitLeopardContext("Server=192.168.1.96;User Id=sa;Password=Tulde30#;Database=ShitLeopard;"))
            //{
            //    //seasons
            //    db.BulkInsert<Season>(seasons.ToList());
            //    //episodes
            //    db.BulkInsert(seasons.SelectMany(x => x.Episode).ToList());
            //    //scripts
            //    db.BulkInsert(seasons.SelectMany(x => x.Episode.SelectMany(y => y.Script)).ToList());
            //    //script lines
            //    var lines = seasons.SelectMany(x => x.Episode.SelectMany(y => y.Script.SelectMany(x => x.ScriptLine))).ToList();
            //    db.BulkInsert(lines);
            //    //script words
            //    var words = seasons.SelectMany(x => x.Episode.SelectMany(y => y.Script.SelectMany(x => x.ScriptLine.SelectMany(z => z.ScriptWord)))).ToList();
            //    db.BulkInsert(words);
            //}
            await Host.CreateDefaultBuilder(args)
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

        private static SqlCommand CreateCommand(string text, object p)
        {
            var cmd = new SqlCommand(text);
            cmd.CommandType = System.Data.CommandType.Text;
            foreach (var item in p.GetType().GetProperties())
            {
                cmd.Parameters.AddWithValue(item.Name, item.GetValue(p));
            }
            return cmd;
        }
    }
}

//var test = File.ReadAllText(@"C:\Development\Spotify.Playlister\src\Spotify.Playlister\bin\Debug\netcoreapp2.2\TPBoys.json");
//var episodes = JsonConvert.DeserializeObject<List<Episode>>(test);
//Console.WriteLine("Processing " + episodes.Count + " episodes");

//using (var client = new SqlConnection("Server=localhost;Database=TPBoys;User Id=sa;Password=Tulde30#;"))
//{
//    client.Open();
//    foreach (var item in episodes)
//    {
//        var cmd = CreateCommand("INSERT INTO Script (Season, Title) VALUES (@season, @title); SELECT @@IDENTITY",
//            new
//            {
//                season = item.Season,
//                title = item.Title
//            });
//        cmd.Connection = client;
//        var pkey = (decimal)cmd.ExecuteScalar();

//        item.Script = item.Script.Replace("<br>", "|");
//        var bodyCmd = CreateCommand("INSERT INTO ScriptContent (ScriptID, Body) values ( @id, @content)",
//            new
//            {
//                id = pkey,
//                content = item.Script
//            });
//        bodyCmd.Connection = client;
//        bodyCmd.ExecuteNonQuery();
//        foreach (var line in item.Script.Split("|".ToCharArray()))
//        {
//            var text = line.Trim();
//            if (text.Contains(Environment.NewLine))
//            {
//            }
//            if (text.Length <= 1)
//            {
//                continue;
//            }
//            Console.WriteLine(text);
//            cmd = new SqlCommand("INSERT INTO ScriptLine (ScriptId,Line) VALUES (@script, @body); SELECT @@Identity");
//            cmd.CommandType = System.Data.CommandType.Text;
//            cmd.Parameters.AddWithValue("script", pkey);
//            cmd.Parameters.AddWithValue("body", text);
//            cmd.Connection = client;
//            cmd.ExecuteNonQuery();
//        }