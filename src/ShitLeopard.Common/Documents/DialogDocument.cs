using MongoDB.Entities;

namespace ShitLeopard.Common.Documents
{
    public class DialogDocument : Entity
    {
        public long DialogLineNumber { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Body { get; set; }
        
        public EpisodeDocument Episode { get; set; }
        public string SeasonId { get; set; }

        public int? Offset { get; set; }
        public double? Score { get; set; }

    }
}