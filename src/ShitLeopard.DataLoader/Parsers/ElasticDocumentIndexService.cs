using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Nest;
using Nest.JsonNetSerializer;
using ShitLeopard.Common.Documents;
using ShitLeopard.DataLoader.Configuration;
using ShitLeopard.DataLoader.Contracts;
using ShitLeopard.DataLoader.Models;

namespace ShitLeopard.DataLoader.Parsers
{
    internal class ElasticDocumentIndexService : IElasticDocumentIndexService
    {
        private readonly ConnectionString _connectionString;
        private readonly IConfiguration _configuration;
        private readonly Options _options;
        private readonly IConsoleLogger _consoleLogger;
        private ElasticClient _elasticClient;

        public ElasticDocumentIndexService(IConfiguration configuration, Options options, IConsoleLogger consoleLogger)
        {
            _configuration = configuration;
            _options = options;
            _consoleLogger = consoleLogger;
        }

        public Task InitAsync()
        {
            var uri = new Uri(_configuration["ElasticHost"]);
            var pool = new SingleNodeConnectionPool(uri);

            var settings = new ConnectionSettings(pool, sourceSerializer: (builtin, settings) => new JsonNetSerializer(builtin, settings, () => new Newtonsoft.Json.JsonSerializerSettings { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore }))
            .EnableDebugMode()
            .PrettyJson()
             .RequestTimeout(TimeSpan.FromMinutes(2));

            _elasticClient = new ElasticClient(settings);

            return Task.CompletedTask;
        }

        //public async Task<int> IndexSeasonsAsync(IEnumerable<Season> seasons)
        //{
        //    //var dialogLines = new List<DialogDocument>();

        //    //var showId = seasons.SelectMany(x => x.Episode.Select(y => y.Show.ShowId)).Distinct().FirstOrDefault();

        //    //var filter = Builders<ShowDocument>.Filter.Eq(x => x.ID, showId);

        //    //var show = await DB.Collection<ShowDocument>().Find(filter).SingleOrDefaultAsync();

        //    //var episodes = seasons.SelectMany(x => x.Episode.Select(y => new { Season = x, Episode = y }).Select(x => new EpisodeDocument
        //    //{
        //    //    EpisodeNumber = x.Episode.Id,
        //    //    OffsetId = x.Episode.OffsetId,
        //    //    Title = x.Episode.Title,
        //    //    SeasonId = $"{ x.Season.Id}",
        //    //    Synopsis = x.Episode.Synopsis,
        //    //    Body = string.Join(" ", x.Episode.Script.Select(x=>x.Body)),
        //    //    Show = show ?? new ShowDocument
        //    //    {
        //    //        ID = showId
        //    //    }
        //    //}));
        //    //await DB.Collection<EpisodeDocument>().InsertManyAsync(episodes);

        //    //var allEpisodes = await DB.Collection<EpisodeDocument>().Find(Builders<EpisodeDocument>.Filter.Eq(x => x.Show.ID, showId)).ToListAsync();

        //    //foreach (var season in seasons)
        //    //{
        //    //    foreach (var episode in season.Episode)
        //    //    {
        //    //        foreach (var line in episode.Script.SelectMany(x => x.ScriptLine))
        //    //        {
        //    //            var match = allEpisodes.Find(x => x.EpisodeNumber == episode.Id);
        //    //            var dialog = new DialogDocument
        //    //            {
        //    //                DialogLineNumber = line.Id,
        //    //                Body = line.Body,
        //    //                End = line.End,
        //    //                Start = line.Start,
        //    //                Episode = match,
        //    //                Offset = line.Offset
        //    //            };
        //    //            dialogLines.Add(dialog);
        //    //        }
        //    //    }
        //    //}

        //    //await DB.Collection<DialogDocument>().InsertManyAsync(dialogLines);

        //    //  var lines = seasons.SelectMany(x => x.Episode.SelectMany(y => y.Script.SelectMany(z => z.ScriptLine.Select(a => new { Line = a, Episode = y }))));
        //    //var scriptLines = lines.Select(x => new LineDocument
        //    //{
        //    //    ScriptLineId = x.Line.Id,
        //    //    Body = x.Line.Body,
        //    //    EpisodeId = x.Episode.Id,
        //    //    EpisodeTitle = x.Episode.Title,
        //    //});
        //    //await DB.Collection<LineDocument>().InsertManyAsync(scriptLines);

        //    //return 0;
        //}

        public async Task DropDocumentsAsync()
        {
            await _elasticClient.Indices.DeleteAsync("shows");
            _consoleLogger.Write("Deleted shows Index",  ConsoleColor.Magenta);
           await _elasticClient.Indices.DeleteAsync("seasons");
            _consoleLogger.Write("Deleted seasons Index", ConsoleColor.Magenta);
            await _elasticClient.Indices.DeleteAsync("episodes");
            _consoleLogger.Write("Deleted episodes Index", ConsoleColor.Magenta);
 
        }

        public Task<int> IndexSeasonsAsync(IEnumerable<SeasonDocument> seasons)
        {
            throw new NotImplementedException();
        }

        public Task<int> IndexEpisodesAsync(IEnumerable<EpisodeDocument> episodes)
        {
            throw new NotImplementedException();
        }

        public Task<int> IndexShowsAsync(IEnumerable<ShowDocument> shows)
        {
            throw new NotImplementedException();
        }

        public async Task<(int showsIndexed, int seasonsIndexed, int episodesIndex)> BulkIndexAsync(IEnumerable<Show> shows)
        {
            var showDocuments = new List<ShowDocument>();
            var seasonDocuments = new List<SeasonDocument>();
            var episodeDocuments = new List<EpisodeDocument>();
            foreach (var show in shows)
            {
                var showDocument = new ShowDocument()
                {
                    CreateTime = DateTime.UtcNow,
                    UpdateTime = DateTime.UtcNow,
                    DocumentId = show.ShowId,
                    Description = show.Title,
                    Title = show.Title,
                };
                showDocuments.Add(showDocument);

                foreach (var season in show.Seasons)
                {
                    var seasonDocument = new SeasonDocument(showDocument)
                    {
                        CreateTime = DateTime.UtcNow,
                        UpdateTime = DateTime.UtcNow,
                        DocumentId = $"{showDocument.DocumentId}{season.Id}",
                        SeasonNumber = season.Id,
                        Title = season.Title,
                    };
                    seasonDocuments.Add(seasonDocument);

                    episodeDocuments.AddRange(season.Episode.Select(x => new EpisodeDocument(seasonDocument, showDocument)
                    {
                        CreateTime = DateTime.UtcNow,
                        UpdateTime = DateTime.UtcNow,
                        DocumentId = $"{showDocument.DocumentId}{season.Id}{x.Id}",
                        Body = string.Join("", x.Script.Select(y => y.Body)),
                        EpisodeNumber = x.Id,
                        OffsetId = x.OffsetId,

                        Synopsis = x.Synopsis,
                        Title = x.Title
                    }));
                }
            }
            var response1 = await _elasticClient.BulkAsync(b => b.Index("shows").IndexMany(showDocuments, (descriptor, show) => { return descriptor; }));
            var response2 = await _elasticClient.BulkAsync(b => b.Index("seasons").IndexMany(seasonDocuments, (descriptor, show) => { return descriptor; }));
            var response3 = await _elasticClient.BulkAsync(b => b.Index("episodes").IndexMany(episodeDocuments, (descriptor, show) => { return descriptor; }));
            return (response1.Items.Count, response2.Items.Count, response3.Items.Count);
        }

   
    }
}