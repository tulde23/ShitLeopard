using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using HtmlAgilityPack;

namespace ShitLeopard.DataLoader
{
    /// <summary>
    /// Parses Netflix XML closed caption files
    /// </summary>
    public class XmlClosedCaptionParser
    {
        public static void Parse(string folder)
        {
            var episodes = Directory.GetFiles(folder, "*.html");
            foreach (var episode in episodes)
            {
                var html = new HtmlDocument();
                html.Load(episode);
                var doc = XDocument.Load(episode);
                XNamespace tt = "http://www.w3.org/2006/10/ttaf1";
                /**
                 * xmlns:tt="http://www.w3.org/2006/10/ttaf1"
 xmlns:ttm="http://www.w3.org/2006/10/ttaf1#metadata"
 xmlns:ttp="http://www.w3.org/2006/10/ttaf1#parameter"
 xmlns:tts="http://www.w3.org/2006/10/ttaf1#styling"
 ttp:tickRate="10000000"
 ttp:timeBase="media"
 xml:lang="en"
 xmlns="http://www.w3.org/2006/10/ttaf1"

                var
                 * */

                var ps = html.DocumentNode.Descendants("p").ToList();
                foreach (var i in ps)
                {
                    if (i.InnerText.Trim().StartsWith("["))
                    {
                        continue;
                    }
                    var sb = new StringBuilder();
                    foreach (var child in i.ChildNodes)
                    {
                        if (child.Name == "#text")
                        {
                            var text = child.InnerText.Trim().Replace("-", string.Empty);
                            sb.Append(" " + text);
                            if (char.IsPunctuation(text[text.Length - 1]))
                            {
                                sb.Append(" ");
                            }
                        }
                    }
                    Console.WriteLine(sb.ToString());
                }

                var paragraphs = doc.Root.Descendants(tt + "p").ToList();
                foreach (var p in paragraphs)
                {
                    var h = new HtmlDocument();
                    h.LoadHtml(p.Value);

                    Console.WriteLine(p.Value?.Trim());
                }
            }
        }
    }
}