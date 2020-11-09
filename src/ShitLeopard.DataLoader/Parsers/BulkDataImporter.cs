using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Entities;
using ShitLeopard.Common.Documents;
using ShitLeopard.DataLayer.Entities;
using ShitLeopard.DataLoader.Contracts;

namespace ShitLeopard.DataLoader.Parsers
{
    internal class BulkDataImporter : IBulkDataImporter
    {
      

        public async Task<int> ImportAsync(IEnumerable<Season> seasons)
        {
            await DB.InitAsync("ShitLeopard", MongoClientSettings.FromConnectionString("mongodb://admin:Tulde30#@192.168.86.27:27017"));
            var client = new MongoClient("mongodb://admin:Tulde30#@192.168.86.27:27017");
            var db = client.GetDatabase("ShitLeopard");
            Console.WriteLine(db.Settings);
            db.DropCollection("lines");
            db.DropCollection("words");
            db.DropCollection("episodes");
            db.DropCollection("characters");

            var scriptWordCollection = db.GetCollection<WordDocument>("words");
            await scriptWordCollection.Indexes.CreateOneAsync(new CreateIndexModel<WordDocument>(Builders<WordDocument>.IndexKeys.Text(x=>x.Text)));

            var scriptLineCollection = db.GetCollection<LineDocument>("lines");
            await scriptLineCollection.Indexes.CreateOneAsync(new CreateIndexModel<LineDocument>(Builders<LineDocument>.IndexKeys.Text(k => k.Body).Text(k => k.EpisodeTitle)));

            var episodeCollection = db.GetCollection<EpisodeDocument>("episodes");
            await episodeCollection.Indexes.CreateOneAsync(new CreateIndexModel<EpisodeDocument>(Builders<EpisodeDocument>.IndexKeys.Text(k => k.Synopsis).Text(k => k.Title)));

            var characterCollection = db.GetCollection<CharacterDocument>("characters");
            await characterCollection.Indexes.CreateOneAsync(new CreateIndexModel<CharacterDocument>(Builders<CharacterDocument>.IndexKeys.Text(k => k.Name).Text(x => x.PlayedBy)));

            var words = seasons.SelectMany(x => x.Episode.SelectMany(y => y.Script.SelectMany(z => z.ScriptLine.SelectMany(a => a.ScriptWord))));
            await scriptWordCollection.InsertManyAsync(words.Select(x => new WordDocument
            {
                Id = x.Id,
                ScriptLineId = x.ScriptLineId,
                Text = x.Word
            }));

            var lines = seasons.SelectMany(x => x.Episode.SelectMany(y => y.Script.SelectMany(z => z.ScriptLine.Select(a => new { Line = a, Episode = y }))));
            await scriptLineCollection.InsertManyAsync(lines.Select(x => new LineDocument
            {
                Id = x.Line.Id,
                Body = x.Line.Body,
                EpisodeId = x.Episode.Id,
                EpisodeTitle = x.Episode.Title,

                Character = x.Line.Character == null ? new CharacterDocument() : new CharacterDocument
                {
                    Aliases = x.Line.Character.Aliases,
                    Id = x.Line.Character.Id,
                    Name = x.Line.Character.Name,
                    Notes = x.Line.Character.Notes,
                    PlayedBy = x.Line.Character.PlayedBy
                }
            }));

            var episodes = seasons.SelectMany(x => x.Episode);
            await episodeCollection.InsertManyAsync(episodes.Select(x => new EpisodeDocument
            {
                Id = x.Id,
                OffsetId = x.OffsetId,
                SeasonId = x.SeasonId,
                Synopsis = x.Synopsis,
                Title = x.Title
            }));

            return 0;
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