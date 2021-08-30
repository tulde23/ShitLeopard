using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using ShitLeopard.DataLoader.Configuration;
using ShitLeopard.DataLoader.Contracts;
using ShitLeopard.DataLoader.Models;

namespace ShitLeopard.DataLoader.Metadata
{
    /// <summary>
    /// grabs episode titles and synopsis from wikipedia bitch.
    /// </summary>
    /// <seealso cref="ShitLeopard.DataLoader.Contracts.IMetadataProvider" />
    internal class WikipediaMetadataProvider : IMetadataProvider
    {
        private readonly IConsoleLogger _consoleLogger;
        private string _wikipediaAddress = "https://en.wikipedia.org/wiki/";

        public WikipediaMetadataProvider(IConfiguration configuration, IConsoleLogger consoleLogger)
        {
            _wikipediaAddress = configuration["wikipedia"] ?? _wikipediaAddress;
            _consoleLogger = consoleLogger;
        }

        public async Task<IEnumerable<EpisodeMetadata>> GetMetadataAsync(ShowConfiguration showConfiguration)
        {
            List<EpisodeMetadata> episodes = new List<EpisodeMetadata>();
            using (var client = new HttpClient())
            {
                var html = await client.GetAsync($"{_wikipediaAddress}{showConfiguration.Metadata.ShowName}");
                var content = await html.Content.ReadAsStringAsync();
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(content);

                var classes = new List<string>(showConfiguration.Metadata.ClassSelectors);
                var tables = doc.DocumentNode.Descendants("table").Where(x => x.GetAttributeValue("class", "").Equals(string.Join(" ", classes)));
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
                            if (synopsisNode.Name == "#text")
                            {
                                synopsis = synopsisNode.InnerText;
                                break;
                            }
                            if (synopsisNode.Name.Equals("tr", StringComparison.OrdinalIgnoreCase) &&
                                synopsisNode.GetAttributeValue("class", "").Equals("expand-child"))
                            {
                                synopsis = synopsisNode.Descendants("td")?.FirstOrDefault()?.InnerText?.Trim();
                                break;
                            }
                        }

                        int.TryParse(idInSeason, out var id2);
                        _consoleLogger.Write($"{eid} - {summary}");

                        if (long.TryParse(eid, out var id))
                        {
                            episodes.Add(new EpisodeMetadata
                            {
                                Id = id,
                                OffsetId = id2,
                                Title = summary,
                                Synopsis = synopsis,
                            });

                            if (id == showConfiguration.Metadata.LastEpisodeId)
                            {
                                _consoleLogger.Write("Found " + episodes.Count);
                                return episodes;
                            }
                        }
                    }
                }
            }
            _consoleLogger.Write("Found " + episodes.Count);
            return episodes;
        }
    }
}