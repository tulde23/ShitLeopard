using System;
using System.Data.SqlClient;
using System.Linq;
using EFCore.BulkExtensions;
using ShitLeopard.DataLayer.Entities;

namespace ShitLeopard.DataLoader
{
    public class Program
    {
        private const string _defaultPath = @"C:\Development\Spotify.Playlister\src\Spotify.Playlister\bin\Debug\netcoreapp2.2\TPBoys.json";

        private static void Main(string[] args)
        {
            Console.WriteLine("Starting....");

            XmlClosedCaptionParser.Parse(@"C:\Development\ShitLeopard\ClosedCaptions\s1");

            var json = DataParser.GetDocument(_defaultPath);
            var seasons = DataParser.GetSeasons(json);
            using (var db = new ShitLeopardContext("Server=192.168.1.96;User Id=sa;Password=Tulde30#;Database=ShitLeopard;"))
            {
                //seasons
                db.BulkInsert<Season>(seasons.ToList());
                //episodes
                db.BulkInsert(seasons.SelectMany(x => x.Episode).ToList());
                //scripts
                db.BulkInsert(seasons.SelectMany(x => x.Episode.SelectMany(y => y.Script)).ToList());
                //script lines
                var lines = seasons.SelectMany(x => x.Episode.SelectMany(y => y.Script.SelectMany(x => x.ScriptLine))).ToList();
                db.BulkInsert(lines);
                //script words
                var words = seasons.SelectMany(x => x.Episode.SelectMany(y => y.Script.SelectMany(x => x.ScriptLine.SelectMany(z => z.ScriptWord)))).ToList();
                db.BulkInsert(words);
            }
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