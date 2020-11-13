using MongoDB.Entities;

namespace ShitLeopard.Common.Documents
{
    public class EpisodeDocument : Entity
    {
        public EpisodeDocument()
        {
        }
        public long EpisodeNumber { get; set; }
        public int? OffsetId { get; set; }
        public string Title { get; set; }
        public string SeasonId { get; set; }
        public string Synopsis { get; set; }
    }
}