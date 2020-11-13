using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using ShitLeopard.DataLayer.Entities;

namespace ShitLeopard.DataLoader
{
    public static class DataParser
    {
        public static string GetDocument(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            return null;
        }

        public static IEnumerable<Season> GetSeasons(string json)
        {
            var episodes = JsonConvert.DeserializeObject<List<Episode>>(json);

            List<Season> seasons = new List<Season>();
            long scriptId = 1;
            long scriptLineId = 0;
            long scriptWordId = 1;
            long episodeCounter = 0;
            foreach (var item in episodes)
            {
                var season = GetSeasonEpisodeFromTitle(item.Season);
                var currentSeason = seasons.SingleOrDefault(x => x.Id == season.season);
                if (currentSeason == null)
                {
                    currentSeason = new Season();
                    currentSeason.Id = season.season;
                    currentSeason.Title = $"Season {season.season}";
                    seasons.Add(currentSeason);
                    currentSeason.Episode = new List<DataLayer.Entities.Episode>();
                }

                var currentEpisode = currentSeason.Episode.SingleOrDefault(x => x.Id == season.episode);
                if (currentEpisode == null)
                {
                    currentEpisode = new DataLayer.Entities.Episode
                    {
                        Script = new List<Script>(),
                        Season = currentSeason,
                        SeasonId = season.season,
                        Id = ++episodeCounter,
                        Title = $"Episode: {season.episode} - {item.Title}"
                    };
                    currentSeason.Episode.Add(currentEpisode);
                }

                currentEpisode.Script = new List<Script>();
                var script = new Script
                {
                    Id = scriptId++,
                    Body = item.Script.Replace("<br>", "|"),
                    EpisodeId = currentEpisode.Id,
                    Episode = currentEpisode,
                    ScriptLine = new List<ScriptLine>()
                };
                currentEpisode.Script.Add(script);

                script.ScriptLine = new List<ScriptLine>(script.Body.Split("|".ToCharArray()).Select(x =>
                    new ScriptLine
                    {
                        Id = ++scriptLineId,
                        Body = x,
                        ScriptId = script.Id
                    }));
            }
            return seasons;
        }

        public class Episode
        {
            public string Season { get; set; }
            public string Title { get; set; }
            public string Script { get; set; }
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

        private static (int season, int episode) GetSeasonEpisodeFromTitle(string x)
        {
            string pattern = @"([s])(\d+)([e])(\d+)";

            RegexOptions options = RegexOptions.Multiline;

            var matches = Regex.Match(x, pattern, options);
            if (matches != null && matches.Groups.Count == 5)
            {
                return (int.Parse(matches.Groups[2].Value), int.Parse(matches.Groups[4].Value));
            }

            return (0, 0);
        }
    }
}