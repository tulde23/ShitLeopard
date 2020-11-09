using System.ComponentModel.DataAnnotations;

namespace ShitLeopard.Common.Documents
{
    [Display(Name = "lines")]
    public class LineDocument
    {
        public long Id { get; set; }
        public string Body { get; set; }
        public long? CharacterId { get; set; }

        public long EpisodeId{get;set;}

        public string EpisodeTitle{ get; set; }

        public virtual CharacterDocument Character { get; set; }
    }
}