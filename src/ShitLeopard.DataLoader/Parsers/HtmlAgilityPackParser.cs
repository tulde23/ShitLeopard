using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using ShitLeopard.DataLayer.Entities;
using ShitLeopard.DataLoader.Contracts;

namespace ShitLeopard.DataLoader.Parsers
{
    internal class HtmlAgilityPackParser : ISeasonParser
    {
        private readonly Options _options;
        private readonly ILogger<HtmlAgilityPackParser> _logger;

        public HtmlAgilityPackParser(Options options, ILogger<HtmlAgilityPackParser> logger)
        {
            _options = options;
            _logger = logger;
        }

        public Task<IEnumerable<Season>> GetSeasonsAsync(DirectoryInfo directoryInfo)
        {
            var documents = directoryInfo.GetFiles("*.html", SearchOption.AllDirectories);
            foreach (var document in documents.GroupBy(x => x.Directory.Name).OrderBy(x => int.Parse(x.Key.Replace("s", string.Empty))))
            {
                //first level directory will be the season
                var season = document.Key;
                _logger.LogInformation($"Processing Season: {season}.  Contains {document.Count()} closed caption files.");
                foreach (var closedCaptionFile in document)
                {
                    var html = new HtmlDocument();
                    html.Load(closedCaptionFile.FullName);

                    var test = html.DocumentNode.Descendants("p").ToList();
                    //just because a p tag exists, it doesn't neccessarily mean it's a complete quote.  Text may not end with punctuation, so we must continue to the next block
                    var first = new Paragraph(html.DocumentNode.Descendants("p").FirstOrDefault());
                    if( first == null)
                    {
                        continue;
                    }
                    var root = NextParagrah(first);

                    while ((root = NextParagrah(root)) != null)
                    {
                        root.Parse();
                        Console.WriteLine(root.Text);
                        root = root.NextSibling;
                    }
                    //foreach (var i in ps)
                    //{
                    //    if (i.InnerText.Trim().StartsWith("["))
                    //    {
                    //        continue;
                    //    }
                    //    var sb = new StringBuilder();
                    //    foreach (var child in i.ChildNodes)
                    //    {
                    //        if (child.Name == "#text")
                    //        {
                    //            var text = child.InnerText.Trim().Replace("-", string.Empty);
                    //            sb.Append(" " + text);
                    //            if (char.IsPunctuation(text[text.Length - 1]))
                    //            {
                    //                sb.Append(" ");
                    //            }
                    //        }
                    //    }
                    //    Console.WriteLine(sb.ToString());
                    //}
                }
                break;
            }
            return Task.FromResult(Enumerable.Empty<Season>());
        }

        private Paragraph NextParagrah(Paragraph node)
        {
            Paragraph p = node;
            if (p.Name.Equals("p"))
            {
                if (p.InnerText.Trim().StartsWith("["))
                {
                    return NextParagrah(p.NextSibling);
                }
                return p;
            }
            while ((p = p.NextSibling) != null)
            {
                if( p.HtmlNode == null)
                {
                    break;
                }
                if (p.InnerText.Trim().StartsWith("["))
                {
                    continue;
                }
                if (p.Name.Equals("p"))
                {
                    return p;
                }
            }
            return null;
        }

        public class Paragraph
        {
            public string Name
            {
                get
                {
                    return HtmlNode?.Name;
                }
            }

            public string InnerText
            {
                get
                {
                    return HtmlNode?.InnerText;
                }
            }

            public string InnerHtml
            {
                get
                {
                    return HtmlNode?.InnerHtml;
                }
            }

            public Paragraph NextSibling
            {
                get
                {
                    return new Paragraph(this.HtmlNode.NextSibling);
                }
            }

            public bool EndsWithPunctuation { get; private set; }
            public string Text { get; private set; }

            public Paragraph(HtmlNode htmlNode)
            {
                HtmlNode = htmlNode;
            }

            public void Parse()
            {
                var sb = new StringBuilder();
                foreach (var child in HtmlNode.ChildNodes)
                {
                    if (child.Name == "#text")
                    {
                        var text = child.InnerText.Trim().Replace("-", string.Empty);
                        sb.Append(" " + text);
                    }
                }
                var t = sb.ToString();
                this.Text = t.Trim();
                EndsWithPunctuation = char.IsPunctuation(this.Text[this.Text.Length - 1]);
            }

            public HtmlNode HtmlNode { get; }
        }
    }
}