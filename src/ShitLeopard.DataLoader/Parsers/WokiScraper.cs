using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ShitLeopard.DataLayer.Entities;
using ShitLeopard.DataLoader.Contracts;

namespace ShitLeopard.DataLoader.Parsers
{
    internal class WokiScraper : IWikiScraper
    {
        private static string _address = @"https://en.wikipedia.org/wiki/List_of_Trailer_Park_Boys_episodes";

        public async Task<IEnumerable<Episode>> GetEpisodesAsync()
        {
            List<Episode> episodes = new List<Episode>();
            using (var client = new HttpClient())
            {
                var html = await client.GetAsync(_address);
                var content = await html.Content.ReadAsStringAsync();
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(content);

                var tables = doc.DocumentNode.Descendants("table").Where(x => x.GetAttributeValue("class", "").Equals("wikitable plainrowheaders wikiepisodetable"));
                foreach (var table in tables)
                {
                    foreach (var tr in table.Descendants("tr").Where(x => x.GetAttributeValue("class", "").Equals("vevent")))
                    {
                        var th = tr.Descendants("th").FirstOrDefault();
                        var td = tr.Descendants("td").FirstOrDefault(x => x.GetAttributeValue("class", "").Equals("summary"));
                        var eid = th?.InnerText?.Trim();
                        var summary = td?.InnerText;
                        Console.WriteLine($"{eid} - {summary}");

                        if (long.TryParse(eid, out var id))
                        {
                            episodes.Add(new Episode
                            {
                                Id = id,
                                Title = summary
                            });

                            if (id == 105)
                            {
                                Console.WriteLine("Found " + episodes.Count);
                                return episodes;
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Found " + episodes.Count);
            return episodes;
        }
    }
}