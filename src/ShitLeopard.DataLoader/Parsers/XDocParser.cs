using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using ShitLeopard.DataLayer.Entities;
using ShitLeopard.DataLoader.Configuration;
using ShitLeopard.DataLoader.Contracts;

namespace ShitLeopard.DataLoader.Parsers
{
    internal class XDocParser : ISeasonParser
    {
        private readonly Options _options;
        private readonly ILogger<XDocParser> _logger;
        private readonly IConsoleLogger _consoleLogger;
        private readonly List<XNamespace> _ns = new List<XNamespace>()
        {
          "http://www.w3.org/2006/10/ttaf1",
            "http://www.w3.org/ns/ttml"
        };

        public XDocParser(Options options, ILogger<XDocParser> logger, IConsoleLogger consoleLogger)
        {
            _options = options;
            _logger = logger;
            _consoleLogger = consoleLogger;
        }

        public Task<IEnumerable<Season>> GetSeasonsAsync(ShowConfiguration showConfiguration)
        {
            var rootDir = new DirectoryInfo(showConfiguration.RootFolder);

            var directoryInfo = new DirectoryInfo(Path.Combine(rootDir.FullName, showConfiguration.ClosedCaptionsPath));
            var documents = directoryInfo.GetFiles($"*{showConfiguration.ClosedCaptionsFileExtension}", SearchOption.AllDirectories);
            var seasons = new List<Season>();
            int episodeCount = 1;
            int lineCounter = 1;
            int scriptCounter = 1;
            long wordCounter = 1;
            foreach (var document in documents.GroupBy(x => x.Directory.Name).OrderBy(x => int.Parse(x.Key.Replace("s", string.Empty))))
            {
                //first level directory will be the season
                var season = document.Key;
                _consoleLogger.Write($"Processing Season: {season}.  Contains {document.Count()} closed caption files.");
                var seasonEntity = new Season
                {
                    Id = int.Parse(document.Key.Replace("s", string.Empty)),
                    Title = document.Key,
                    Episode = new List<Episode>()
                };
                seasons.Add(seasonEntity);
                foreach (var closedCaptionFile in document)
                {
                    var episode = new Episode()
                    {
                        Id = episodeCount++,
                        SeasonId = seasonEntity.Id,
                        Title = string.Empty
                    };

                    seasonEntity.Episode.Add(episode);
                    var doc = XDocument.Load(closedCaptionFile.FullName);

                    XElement rootNode = null;
                    foreach (var n in _ns)
                    {
                        rootNode = doc.Root.Descendants(n + "p").FirstOrDefault();
                        if (rootNode != null)
                        {
                            break;
                        }
                    }
                    var node = new Paragraph(rootNode);
                    node.Season = seasonEntity.Id;
                    node.Episode = episode.Id;
                    var scriptBuilder = new StringBuilder();
                    var lines = new List<Paragraph>();
                    if (node?.Node == null)
                    {
                        _consoleLogger.Write("no root node");
                        continue;
                    }
                    try
                    {
                        var root = NextParagrah(node);

                        while ((root = NextParagrah(root)) != null)
                        {
                            scriptBuilder.AppendLine(root.Text);
                            lines.Add(root);
                            root = root.NextNode;
                        }
                        episode.Script = new List<Script>()
                        {
                            new Script
                            {
                                 Id = scriptCounter,
                                  Body = scriptBuilder.ToString(),
                                  EpisodeId = episode.Id,
                                  ScriptLine = lines.Select( s=> new ScriptLine
                                  {
                                      Id= lineCounter++,
                                       Body = s.Text,
                                       End = s.EndLocation,
                                       Start = s.StartLocation,
                                       ScriptId = scriptCounter,
                                       Offset = s.Offset ?? 0
                                  }).ToList()
                            }
                        };
                        scriptCounter++;
                    }
                    catch (Exception ex)
                    {
                        _consoleLogger.Write(ex.StackTrace);
                    }

                    _consoleLogger.Write($"\tEpisode {episode.Id} has  {episode.Script?.FirstOrDefault()?.ScriptLine?.Count} Lines.");
                }
            }

            return Task.FromResult(seasons.AsEnumerable());
        }

        private static List<ScriptWord> GetWordsFromLine(string line, long scriptLineId, ref long idCounter)
        {
            List<ScriptWord> words = new List<ScriptWord>();
            Queue<char> letters = new Queue<char>();
            foreach (var c in line)
            {
                if (Char.IsWhiteSpace(c) || (Char.IsPunctuation(c) && c != '\''))
                {
                    if (letters.Any())
                    {
                        var sb = new StringBuilder();
                        while (letters.Count > 0)
                        {
                            sb.Append(letters.Dequeue());
                        }
                        words.Add(new ScriptWord
                        {
                            Id = ++idCounter,
                            ScriptLineId = scriptLineId,
                            Word = sb.ToString(),
                        });
                    }
                }
                else if (Char.IsSeparator(c) || (Char.IsPunctuation(c) && c != '\''))
                {
                    continue;
                }
                else
                {
                    letters.Enqueue(c);
                }
            }
            return words;
        }

        private Paragraph NextParagrah(Paragraph node)
        {
            Paragraph p = node;

            if (p?.Name?.Equals("p") == true)
            {
                if (p?.Text?.Length <= 0)
                {
                    return NextParagrah(p.NextNode);
                }
                return p;
            }
            while ((p = p?.NextNode) != null)
            {
                if (p.Node == null)
                {
                    break;
                }
                if (p?.Text?.Length <= 0)
                {
                    continue;
                }
                if (p?.Name?.Equals("p") == true)
                {
                    if (!p.EndsWithPunctuation)
                    {
                        return new Paragraph(p);
                    }
                    return p;
                }
            }
            return null;
        }

        public bool EndsWithPunctuation(string x)
        {
            if (string.IsNullOrEmpty(x))
            {
                return false;
            }
            char lastItem = x[x.Length - 1];
            return char.IsPunctuation(lastItem) && lastItem != ',';
        }

        public class Paragraph
        {
            public long Season { get; set; }
            public long Episode { get; set; }
            public string EndLocation { get; set; }

            public string StartLocation { get; set; }

            public int? Offset { get; set; } = 0;

            public Paragraph(XNode element)
            {
                Node = element;
                var e = Node as XElement;
                if (e == null)
                {
                    return;
                }
                var sb = new StringBuilder(e.Value.Trim())
                          .Replace("-", string.Empty)
                          .Replace("\n", string.Empty);
                var text = sb.ToString().Trim();

                text = Regex.Replace(text, @"(\[(.+)\])+", string.Empty);
                text = Regex.Replace(text, @"(\s{1,})", " ").ToLower();
                if (text.Length > 1)
                {
                    sb = new StringBuilder(text);
                    sb[0] = char.ToUpper(sb[0]);
                    Text = sb.ToString();
                }
                else
                {
                    Text = text;
                }

                if (!string.IsNullOrEmpty(Text))
                {
                    EndsWithPunctuation = EndsWith(Text);
                }
                EndLocation = e.Attribute("end")?.Value;
                StartLocation = e.Attribute("begin")?.Value;
                Offset = Offset + Text.Length;
            }

            public Paragraph(Paragraph paragraph)
            {
                Node = paragraph?.NextNode?.Node;
                EndLocation = paragraph?.EndLocation;
                Season = paragraph?.Season ?? 0;
                Episode = paragraph?.Episode ?? 0;
                Paragraph next = paragraph;
                Text = paragraph?.Text;
                Offset += paragraph?.Offset;
                var sb = new StringBuilder(" ");
                sb.Append(Text);
                while ((next = next?.NextNode) != null)
                {
                    if (!string.IsNullOrWhiteSpace(next.Text))
                    {
                        sb.Append(" ").Append(next.Text);
                        Text = sb.ToString();
                        EndsWithPunctuation = EndsWith(next.Text);
                        if (EndsWithPunctuation)
                        {
                            Node = next.Node;
                            break;
                        }
                    }
                }
            }

            public string Name
            {
                get
                {
                    var e = Node as XElement;
                    if (e != null)
                    {
                        return e?.Name?.LocalName;
                    }
                    return string.Empty;
                }
            }

            public string Value
            {
                get
                {
                    var e = Node as XElement;
                    if (e != null)
                    {
                        return e.Value;
                    }
                    return string.Empty;
                }
            }

            public bool EndsWith(string x)
            {
                if (string.IsNullOrEmpty(x))
                {
                    return false;
                }
                return char.IsPunctuation(x[x.Length - 1]);
            }

            public bool EndsWithPunctuation { get; private set; }

            public XNode Node { get; }

            public string Text { get; private set; }

            public Paragraph NextNode
            {
                get
                {
                    if (this.Node?.NextNode == null)
                    {
                        return null;
                    }
                    return new Paragraph(this.Node.NextNode);
                }
            }
        }
    }
}