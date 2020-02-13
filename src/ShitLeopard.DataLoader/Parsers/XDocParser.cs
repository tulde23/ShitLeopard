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

        public XDocParser(Options options, ILogger<XDocParser> logger)
        {
            _options = options;
            _logger = logger;
        }

        public Task<IEnumerable<Season>> GetSeasonsAsync(DirectoryInfo directoryInfo)
        {
            XNamespace tt = "http://www.w3.org/2006/10/ttaf1";
            var documents = directoryInfo.GetFiles("*.html", SearchOption.AllDirectories);
            foreach (var document in documents.GroupBy(x => x.Directory.Name).OrderBy(x => int.Parse(x.Key.Replace("s", string.Empty))))
            {
                //first level directory will be the season
                var season = document.Key;
                _logger.LogInformation($"Processing Season: {season}.  Contains {document.Count()} closed caption files.");
                foreach (var closedCaptionFile in document)
                {
                    var doc = XDocument.Load(closedCaptionFile.FullName);
                    var node = new Paragraph(doc.Root.Descendants(tt + "p").FirstOrDefault());
                    if( node == null)
                    {
                        Console.WriteLine("no root node");
                    }
                    try
                    {
                        var root = NextParagrah(node);
                        while ((root = NextParagrah(root)) != null)
                        {
                            Console.WriteLine(root.Text);
                            root = root.NextNode;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }
            }

            return Task.FromResult(Enumerable.Empty<Season>());
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