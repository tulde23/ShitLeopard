using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
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
                        var td2 = tr.Descendants("td").FirstOrDefault(x => x.GetAttributeValue("style", "").Equals("text-align:center"));
                        var td = tr.Descendants("td").FirstOrDefault(x => x.GetAttributeValue("class", "").Equals("summary"));
                        var eid = th?.InnerText?.Trim();
                        var idInSeason = td2?.InnerText.Trim();
                        var summary = td?.InnerText;
                        string synopsis = string.Empty;
                        HtmlNode synopsisNode = null;
                        while ((synopsisNode = tr.NextSibling) != null)
                        {
                            if (synopsisNode.Name.Equals("tr", StringComparison.OrdinalIgnoreCase) &&
                                synopsisNode.GetAttributeValue("class", "").Equals("expand-child"))
                            {
                                synopsis = synopsisNode.Descendants("td")?.FirstOrDefault()?.InnerText?.Trim();
                                break;
                            }
                        }

                        int.TryParse(idInSeason, out var id2);
                        Console.WriteLine($"{eid} - {summary}");

                        if (long.TryParse(eid, out var id))
                        {
                            episodes.Add(new Episode
                            {
                                Id = id,
                                OffsetId = id2,
                                Title = summary,
                                Synopsis = synopsis
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

        public Task RunAsync()
        {
            throw new NotImplementedException();
        }
    }
}