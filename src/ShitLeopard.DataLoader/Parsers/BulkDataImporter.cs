using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Entities;
using ShitLeopard.Common.Documents;
using ShitLeopard.DataLayer.Entities;
using ShitLeopard.DataLoader.Configuration;
using ShitLeopard.DataLoader.Contracts;

namespace ShitLeopard.DataLoader.Parsers
{
    internal class BulkDataImporter : IShowBulkDataImporter
    {
        private readonly ConnectionString _connectionString;
        private readonly Options _options;
        private readonly IConsoleLogger _consoleLogger;

        public BulkDataImporter(ConnectionString connectionString, Options options, IConsoleLogger consoleLogger)
        {
            _connectionString = connectionString;
            _options = options;
            _consoleLogger = consoleLogger;
        }

        public async Task InitAsync()
        {
            await DB.InitAsync("ShitLeopard", MongoClientSettings.FromConnectionString(_connectionString.Value));
            _consoleLogger.Write($"Initialized Shitleopard db connection: {_connectionString.Value}", ConsoleColor.Magenta);
        }

        public async Task<int> ImportAsync(IEnumerable<Season> seasons)
        {
            var dialogLines = new List<DialogDocument>();

            var showId = seasons.SelectMany(x => x.Episode.Select(y => y.Show.ShowId)).Distinct().FirstOrDefault();

            var filter = Builders<ShowDocument>.Filter.Eq(x => x.ID, showId);

            var show = await DB.Collection<ShowDocument>().Find(filter).SingleOrDefaultAsync();

            var episodes = seasons.SelectMany(x => x.Episode.Select(y => new { Season = x, Episode = y }).Select(x => new EpisodeDocument
            {
                EpisodeNumber = x.Episode.Id,
                OffsetId = x.Episode.OffsetId,
                Title = x.Episode.Title,
                SeasonId = $"{ x.Season.Id}",
                Synopsis = x.Episode.Synopsis,
                Body = string.Join(" ", x.Episode.Script.Select(x=>x.Body)),
                Show = show ?? new ShowDocument
                {
                    ID = showId
                }
            }));
            await DB.Collection<EpisodeDocument>().InsertManyAsync(episodes);

            var allEpisodes = await DB.Collection<EpisodeDocument>().Find(Builders<EpisodeDocument>.Filter.Eq(x => x.Show.ID, showId)).ToListAsync();

            foreach (var season in seasons)
            {
                foreach (var episode in season.Episode)
                {
                    foreach (var line in episode.Script.SelectMany(x => x.ScriptLine))
                    {
                        var match = allEpisodes.Find(x => x.EpisodeNumber == episode.Id);
                        var dialog = new DialogDocument
                        {
                            DialogLineNumber = line.Id,
                            Body = line.Body,
                            End = line.End,
                            Start = line.Start,
                            Episode = match,
                            Offset = line.Offset
                        };
                        dialogLines.Add(dialog);
                    }
                }
            }

            await DB.Collection<DialogDocument>().InsertManyAsync(dialogLines);

              var lines = seasons.SelectMany(x => x.Episode.SelectMany(y => y.Script.SelectMany(z => z.ScriptLine.Select(a => new { Line = a, Episode = y }))));
            var scriptLines = lines.Select(x => new LineDocument
            {
                ScriptLineId = x.Line.Id,
                Body = x.Line.Body,
                EpisodeId = x.Episode.Id,
                EpisodeTitle = x.Episode.Title,
            });
            await DB.Collection<LineDocument>().InsertManyAsync(scriptLines);

            return 0;
        }

        public async Task DropCollectionsAsync()
        {
            await DB.DropCollectionAsync<CharacterDocument>();
            await DB.DropCollectionAsync<TagsDocument>();
            await DB.DropCollectionAsync<DialogDocument>();
            await DB.DropCollectionAsync<EpisodeDocument>();
            await DB.DropCollectionAsync<LineDocument>();
            await DB.DropCollectionAsync<TagsByAddressDocument>();
            await DB.DropCollectionAsync<RequestProfileDocument>();


            await DB.Index<CharacterDocument>()
               .Key(k => k.Name, KeyType.Text)
               .Key(k => k.PlayedBy, KeyType.Text)
               .CreateAsync();

            await DB.Index<TagsDocument>()
               .Key(k => k.Category, KeyType.Text)
               .Key(k => k.Name, KeyType.Text)
               .CreateAsync();
            await DB.Index<EpisodeDocument>()
              .Key(k => k.Title, KeyType.Text)
              .Key(k => k.Synopsis, KeyType.Text)
              .Key(k => k.Body, KeyType.Text)
              .CreateAsync();
            await DB.Index<DialogDocument>()
               .Key(k => k.Body, KeyType.Text)
               .CreateAsync();

            await DB.Index<DialogDocument>()
             .Key(k => k.DialogLineNumber, KeyType.Ascending)
             .CreateAsync();

            await DB.Index<LineDocument>()
        .Key(k => k.EpisodeId, KeyType.Ascending)
        .CreateAsync();

            await DB.Index<TagsByAddressDocument>()
       .Key(k => k.TagId, KeyType.Ascending)
       .CreateAsync();

            _consoleLogger.Write($"Completed dropping of collections and indexes.", ConsoleColor.Magenta);
        }
    }
}