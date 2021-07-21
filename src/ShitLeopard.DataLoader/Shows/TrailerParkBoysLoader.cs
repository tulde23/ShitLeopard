using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Features.Indexed;
using Newtonsoft.Json;
using ShitLeopard.DataLayer.Entities;
using ShitLeopard.DataLoader.Contracts;
using ShitLeopard.DataLoader.Models;

namespace ShitLeopard.DataLoader.Shows
{
    internal class TrailerParkBoysLoader : AbstractBulkDataLoader
    {
        private readonly IWikiScraper _wikiScraper;
        private readonly IIndex<string, ISeasonParser> _index;
        private readonly IShowBulkDataImporter _showBulkDataImporter;

        public TrailerParkBoysLoader(ConsoleApplication consoleApplication, Options options,
            IWikiScraper wikiScraper,
            IIndex<string, ISeasonParser> index,
            IShowBulkDataImporter showBulkDataImporter
            ) : base(consoleApplication, options)
        {
            _wikiScraper = wikiScraper;
            _index = index;
            _showBulkDataImporter = showBulkDataImporter;
        }

        public override async Task ImportAsync()
        {
            var episodes = await _wikiScraper.GetEpisodesAsync(new TrailerParkBoysWikiScrapRequest());
            Console.WriteLine("Downloading Episode Metadata");
            await RunImport(episodes);
        }

        private async Task RunImport(IEnumerable<Episode> episodes)
        {
            await _showBulkDataImporter.InitAsync();
            var service = "xdoc";
            var parser = _index[service];

            bool runFullImport = true;

            IEnumerable<Season> seasons = null;
            if (System.IO.File.Exists("Data.json"))
            {
                var text = System.IO.File.ReadAllText("Data.json");
                seasons = JsonConvert.DeserializeObject<List<Season>>(text);
            }
            else
            {
                Options.ImportDirectory = @"C:\Development\ShitLeopard\src\MediaAssets\ClosedCaptions";
                Console.WriteLine($"Loading ClosedCaption Files....");
                seasons = await parser.GetSeasonsAsync(new DirectoryInfo(Options.ImportDirectory));
                var json = JsonConvert.SerializeObject(seasons, Formatting.Indented);
                System.IO.File.WriteAllText("Data.json", json);
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
                }
            }
            Console.WriteLine($"RecycleIndexes");
         //   await _bulkDataImporter.RecycleIndexes();

            Console.WriteLine($"ImportAsync");
            await _showBulkDataImporter.ImportAsync(seasons);
            ConsoleApplication.TokenSource.Cancel();
        }
    }
}