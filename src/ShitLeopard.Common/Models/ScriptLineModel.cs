namespace ShitLeopard.Api.Models
{
    public class ScriptLineModel
    {
        public string Id { get; set; }
        public string Body { get; set; }
        public virtual CharacterModel Character { get; set; }

        public string EpisodeId { get; set; }

        public int? OffsetId { get; set; }
        public string SeasonId { get; set; }
        public string EpisodeTitle { get; set; }
    }
}