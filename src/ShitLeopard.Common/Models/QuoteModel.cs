namespace ShitLeopard.Api.Models
{
    public class QuoteModel
    {
        public long Id { get; set; }
        public long? CharacterId { get; set; }

        public string EpisodeTitle { get; set; }
        public string EpisodeId { get; set; }

        public long? ScriptLineId { get; set; }
        public string Body { get; set; }
        public int Popularity { get; set; }
    }
}