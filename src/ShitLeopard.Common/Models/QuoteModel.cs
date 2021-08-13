using System.Collections.Generic;

namespace ShitLeopard.Api.Models
{
    public class QuoteModel
    {
        public string Id { get; set; }

        public string EpisodeId { get; set; }
        public long? CharacterId { get; set; }

        public long EpisodeNumber { get; set; }
        public int? EpisodeOffsetId { get; set; }
        public string EpisodeTitle { get; set; }
        public string SeasonId { get; set; }
        public string Synopsis { get; set; }

        public string ShowName { get; set; }

        public long? ScriptLineId { get; set; }
        public List<string> Lines { get; set; } = new List<string>();

        public int Popularity { get; set; }

        public double? Score { get; set; }
    }
    public class QuoteLineModel
    {
        public string Line { get; set; }
        public int Position { get; set; }
    }
}