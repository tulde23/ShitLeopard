namespace ShitLeopard.Api.Models
{
    public class ScriptLineModel
    {
        public long Id { get; set; }
        public string Body { get; set; }
        public virtual CharacterModel Character { get; set; }

        public int? EpisodeId { get; set; }

        public int? OffsetId { get; set; }
        public int? SeasonId { get; set; }
        public string EpisodeTitle { get; set; }
    }
}