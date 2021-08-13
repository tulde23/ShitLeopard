namespace ShitLeopard.DataLoader.Models
{
    public class EpisodeMetadata
    {
        public long Id { get; set; }
        public int? OffsetId { get; set; }
        public string Title { get; set; }
        public long? SeasonId { get; set; }
        public string Synopsis { get; set; }
    }
}