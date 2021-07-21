using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ShitLeopard.DataLayer.Entities;
using ShitLeopard.DataLoader.Contracts;
using ShitLeopard.DataLoader.Models;

namespace ShitLeopard.DataLoader.Shows
{

    class EastboundAndDownLoader : AbstractBulkDataLoader
    {
        private readonly ISeasonParserFactory _seasonParserFactory;
        private readonly IWikiScraper _wikiScraper;
        private readonly IShowBulkDataImporter _showBulkDataImporter;

        public EastboundAndDownLoader(ConsoleApplication consoleApplication, Options options, 
            ISeasonParserFactory seasonParserFactory,
            IWikiScraper wikiScraper, IShowBulkDataImporter showBulkDataImporter) : base(consoleApplication, options)
        {
            _seasonParserFactory = seasonParserFactory;
            _wikiScraper = wikiScraper;
            _showBulkDataImporter = showBulkDataImporter;
        }

        public override async Task ImportAsync()
        {
            var episodes = await _wikiScraper.GetEpisodesAsync( new EastboundAndDownWikiScrapRequest());
            Console.WriteLine("Downloading Episode Metadata");
            await RunImport(episodes);

        }
        private async Task RunImport(IEnumerable<Episode> episodes)
        {
            await _showBulkDataImporter.InitAsync();
            var service = "xdoc";
            var parser = _seasonParserFactory.GetParser(service);

            bool runFullImport = true;

            IEnumerable<Season> seasons = null;
            if (System.IO.File.Exists("EastboundAndDown.json"))
            {
                var text = System.IO.File.ReadAllText("EastboundAndDown.json");
                seasons = JsonConvert.DeserializeObject<List<Season>>(text);
            }
            else
            {
                Options.ImportDirectory = @"C:\Development\ShitLeopard\src\EastboundAndDownSubtitles";
                Console.WriteLine($"Loading ClosedCaption Files....");
                seasons = await parser.GetSeasonsAsync(new DirectoryInfo(Options.ImportDirectory));
                var json = JsonConvert.SerializeObject(seasons, Formatting.Indented);
                System.IO.File.WriteAllText("EastboundAndDown.json", json);
            }

            var allEpisodes = seasons.SelectMany(x => x.Episode.Select(y => new { Episode = y, Season = x })).ToList();
            foreach (var e in episodes)
            {
                var match = allEpisodes.FirstOrDefault(x => x.Episode.Id == e.Id);
                if (match != null)
                {
                    match.Episode.Title = e.Title;
                    match.Episode.Synopsis = e.Synopsis;
                    match.Episode.OffsetId = e.OffsetId;
                    match.Episode.Season = e.Season;
                    match.Episode.Show = e.Show;
                }
            }
            Console.WriteLine($"RecycleIndexes");
           await _showBulkDataImporter.RecycleIndexes();

            Console.WriteLine($"ImportAsync");
            await _showBulkDataImporter.ImportAsync(seasons);
            ConsoleApplication.TokenSource.Cancel();
        }
    }
}
