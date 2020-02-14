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
using ShitLeopard.DataLoader.Contracts;

namespace ShitLeopard.DataLoader.Parsers
{
    internal class XDocParser : ISeasonParser
    {
        private readonly Options _options;
        private readonly ILogger<XDocParser> _logger;

        private readonly List<XNamespace> _ns = new List<XNamespace>()
        {
          "http://www.w3.org/2006/10/ttaf1",
            "http://www.w3.org/ns/ttml"
        };

        public XDocParser(Options options, ILogger<XDocParser> logger)
        {
            _options = options;
            _logger = logger;
        }

        public Task<IEnumerable<Season>> GetSeasonsAsync(DirectoryInfo directoryInfo)
        {
            XNamespace tt = "http://www.w3.org/2006/10/ttaf1";
            var documents = directoryInfo.GetFiles("*.html", SearchOption.AllDirectories);
            var seasons = new List<Season>();
            int episodeCount = 1;
            int lineCounter = 1;
            int scriptCounter = 1;
            long wordCounter = 1;
            foreach (var document in documents.GroupBy(x => x.Directory.Name).OrderBy(x => int.Parse(x.Key.Replace("s", string.Empty))))
            {
                //first level directory will be the season
                var season = document.Key;
               Console.WriteLine($"Processing Season: {season}.  Contains {document.Count()} closed caption files.");
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
                    var scriptBuilder = new StringBuilder();
                    var lines = new List<string>();
                    if (node?.Node == null)
                    {
                        Console.WriteLine("no root node");
                        continue;
                    }
                    try
                    {
                        var root = NextParagrah(node);
                        while ((root = NextParagrah(root)) != null)
                        {
                            scriptBuilder.AppendLine(root.Text);
                            lines.Add(root.Text);
                            root = root.NextNode;
                        }
                        episode.Script = new List<Script>()
                        {
                            new Script
                            {
                                 Id = scriptCounter,
                                  Body = scriptBuilder.ToString(),
                                  ScriptLine = lines.Select( s=> new ScriptLine
                                  {
                                      Id= lineCounter,
                                       Body = s,
                                       ScriptId = scriptCounter,
                                        ScriptWord = GetWordsFromLine(s,lineCounter++, ref wordCounter)
                                  }).ToList()
                            }
                        };
                        scriptCounter++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    Console.WriteLine($"\tEpisode {episode.Id} has  {episode.Script?.FirstOrDefault()?.ScriptLine?.Count} Lines.");
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
            return char.IsPunctuation(x[x.Length - 1]);
        }

        public class Paragraph
        {
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
                Text = Regex.Replace(text, @"(\s{1,})", " ");
                if (!string.IsNullOrEmpty(Text))
                {
                    EndsWithPunctuation = char.IsPunctuation(this.Text[this.Text.Length - 1]);
                }
            }

            public Paragraph(Paragraph paragraph)
            {
                Node = paragraph?.NextNode?.Node;
                //var e = Node as XElement;
                //if (e == null)
                //{
                //    return;
                //}
                //var sb = new StringBuilder(e.Value.Trim())
                //          .Replace("-", string.Empty)
                //          .Replace("\n", string.Empty);
                //var text = sb.ToString().Trim();
                //text = Regex.Replace(text, @"(\[(.+)\])+", string.Empty);
                //Text = Regex.Replace(text, @"(\s{1,})", " ");

                Paragraph next = paragraph;
                Text = paragraph.Text;
                while ((next = next?.NextNode) != null)
                {
                    if (!string.IsNullOrWhiteSpace(next.Text))
                    {
                        Text = Text + " " + next.Text;
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