using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Entities;
using ShitLeopard.Common.Documents;
using ShitLeopard.DataLayer.Entities;
using MongoDB.Driver.Linq;
using ShitLeopard.DataLoader.Contracts;

namespace ShitLeopard.DataLoader.Parsers
{
    internal class BulkDataImporter : IShowBulkDataImporter
    {
        public async Task InitAsync()
        {
            await DB.InitAsync("ShitLeopard", MongoClientSettings.FromConnectionString("mongodb://admin:Tulde30#@192.168.86.27:27017"));
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
                            Episode = match
                        };
                        dialogLines.Add(dialog);
                    }
                }
            }

            await DB.Collection<DialogDocument>().InsertManyAsync(dialogLines);

            //var lines = seasons.SelectMany(x => x.Episode.SelectMany(y => y.Script.SelectMany(z => z.ScriptLine.Select(a => new { Line = a, Episode = y }))));
            //var scriptLines = lines.Select(x => new LineDocument
            //{
            //    ScriptLineId = x.Line.Id,
            //    Body = x.Line.Body,
            //    EpisodeId = x.Episode.Id,
            //    EpisodeTitle = x.Episode.Title,

            //    Character = x.Line.Character == null ? new CharacterDocument() : new CharacterDocument
            //    {
            //        Aliases = x.Line.Character.Aliases,
            //        ID = x.Line.Character.Id,
            //        Name = x.Line.Character.Name,
            //        Notes = x.Line.Character.Notes,
            //        PlayedBy = x.Line.Character.PlayedBy
            //    }
            //});

            //await scriptLineCollection.InsertManyAsync(lines.Select(x => new LineDocument
            //{
            //    ID = x.Line.Id,
            //    Body = x.Line.Body,
            //    EpisodeId = x.Episode.Id,
            //    EpisodeTitle = x.Episode.Title,

            //    Character = x.Line.Character == null ? new CharacterDocument() : new CharacterDocument
            //    {
            //        Aliases = x.Line.Character.Aliases,
            //        ID = x.Line.Character.Id,
            //        Name = x.Line.Character.Name,
            //        Notes = x.Line.Character.Notes,
            //        PlayedBy = x.Line.Character.PlayedBy
            //    }
            //}));

            //var episodes = seasons.SelectMany(x => x.Episode);
            //await episodeCollection.InsertManyAsync(episodes.Select(x => new EpisodeDocument
            //{
            //    ID = x.Id,
            //    OffsetId = x.OffsetId,
            //    SeasonId = x.SeasonId,
            //    Synopsis = x.Synopsis,
            //    Title = x.Title
            //  }));

            return 0;
        }

        public async Task RecycleIndexes()
        {
            await DB.DropCollectionAsync<CharacterDocument>();
            await DB.DropCollectionAsync<TagsDocument>();
            await DB.DropCollectionAsync<DialogDocument>();
            await DB.DropCollectionAsync<EpisodeDocument>();

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
              .CreateAsync();
            await DB.Index<DialogDocument>()
               .Key(k => k.Body, KeyType.Text)
               .CreateAsync();

            await DB.Index<DialogDocument>()
             .Key(k => k.DialogLineNumber, KeyType.Ascending)
             .CreateAsync();
        }

        //public async Task<int> UpdateEpisodes(IEnumerable<Episode> episodes)
        //{
        //    var client = new MongoClient("mongodb://admin:Tulde30#@192.168.86.27:27017");
        //    var db = client.GetDatabase("ShitLeopard");
        //    Console.WriteLine(db.Settings);
        //    var seasonCollection = db.GetCollection<Season>("season");

        //    foreach (var episode in episodes)
        //    {
        //        var filter = Builders<Season>.Filter.Eq(x => x.Episode.First().Id, episode.Id);
        //        var document = await seasonCollection.Find(filter).FirstAsync();

        //        var result = await seasonCollection.FindOneAndUpdateAsync(s => s.Episode.Any(e => e.Id == episode.Id),
        //            Builders<Season>.Update
        //                     .Set(x => x.Episode[-1].Title, episode.Title)
        //                     .Set(x => x.Episode[-1].Synopsis, episode.Synopsis)
        //                     .Set(x => x.Episode[-1].OffsetId, episode.OffsetId));

        //        //var update = Builders<Season>.Update
        //        //     .Set(x => x.Episode.First().Title, episode.Title)
        //        //     .Set(x => x.Episode.First().Synopsis, episode.Synopsis)
        //        //     .Set(x => x.Episode.First().OffsetId, episode.OffsetId);

        //        //seasonCollection.UpdateOne(filter, update);
        //    }
        //    //using (var db = _contextProvider())
        //    //{
        //    //  await   db.BulkUpdateAsync(episodes.ToList(), new BulkConfig
        //    //    {
        //    //        PropertiesToInclude = new List<string>()
        //    //        {
        //    //            "OffsetId",
        //    //            "Synopsis"
        //    //        }
        //    //    });
        //    //}
        //    //return episodes.Count();
        //    return 0;
        //}
    }
}