using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using ShitLeopard.DataLayer.Entities;
using ShitLeopard.DataLoader.Contracts;

namespace ShitLeopard.DataLoader.Parsers
{
    internal class BulkDataImporter : IBulkDataImporter
    {
        private readonly Func<ShitLeopardContext> _contextProvider;

        public BulkDataImporter(Func<ShitLeopardContext> contextProvider)
        {
            _contextProvider = contextProvider;
        }

        public async Task<int> ImportAsync(IEnumerable<Season> seasons)
        {
            using (var db = _contextProvider())
            {
                //seasons
                await db.BulkInsertAsync<Season>(seasons.ToList());
                Console.WriteLine("Imported " + seasons.Count() + " seasons");
                //episodes
                await db.BulkInsertAsync(seasons.SelectMany(x => x.Episode).ToList());
                Console.WriteLine($"Import {seasons.SelectMany(x => x.Episode).Count()} episodes");
                //scripts
                await db.BulkInsertAsync(seasons.SelectMany(x => x.Episode.SelectMany(y => y.Script)).ToList());
                Console.WriteLine($"Import {seasons.SelectMany(x => x.Episode.SelectMany(y => y.Script)).Count()} scripts");

                //script lines
                var lines = seasons.SelectMany(x => x.Episode.SelectMany(y => y.Script.SelectMany(x => x.ScriptLine))).ToList();
                await db.BulkInsertAsync(lines);
                Console.WriteLine($"Import {lines.Count} lines");

                //script words
                var words = seasons.SelectMany(x => x.Episode.SelectMany(y => y.Script.SelectMany(x => x.ScriptLine.SelectMany(z => z.ScriptWord)))).ToList();
                await db.BulkInsertAsync(words);
                Console.WriteLine($"Import {words.Count} words");
            }
            return seasons.Count();
        }

        public async Task<int> UpdateEpisodes(IEnumerable<Episode> episodes)
        {
            using (var db = _contextProvider())
            {
              await   db.BulkUpdateAsync(episodes.ToList(), new BulkConfig
                {
                    PropertiesToInclude = new List<string>()
                    {
                        "OffsetId",
                        "Synopsis"
                    }
                });
            }
            return episodes.Count();
        }
    }
}