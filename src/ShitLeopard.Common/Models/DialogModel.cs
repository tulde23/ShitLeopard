using System;
using System.Collections.Generic;
using System.Text;

namespace ShitLeopard.Common.Models
{
    public class DialogModel
    {
        public string Id { get; set; }
        public long DialogLineNumber { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Body { get; set; }
        public long EpisodeNumber { get; set; }
        public int? EpisodeOffsetId { get; set; }
        public string EpisodeTitle { get; set; }
        public string SeasonId { get; set; }
        public string Synopsis { get; set; }

        public string ShowName { get; set; }
    }
}
