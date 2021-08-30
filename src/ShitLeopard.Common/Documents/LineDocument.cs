using System.ComponentModel.DataAnnotations;


namespace ShitLeopard.Common.Documents
{
    [Display(Name = "lines")]
    public class LineDocument : ElasticDocument
    {
        public long ScriptLineId { get; set; }
        public string Body { get; set; }
        public string CharacterId { get; set; }

        public long EpisodeId { get; set; }

        public string EpisodeTitle { get; set; }

        public virtual CharacterDocument Character { get; set; }
    }
}