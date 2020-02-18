using System.Collections.Generic;

namespace ShitLeopard.Api.Models
{
    public class ScriptLineModel
    {
        public long Id { get; set; }
        public string Body { get; set; }
        public long ScriptId { get; set; }
        public long? CharacterId { get; set; }

        public virtual CharacterModel Character { get; set; }
        public virtual ScriptModel Script { get; set; }
        public virtual ICollection<ScriptWordModel> ScriptWord { get; set; }


        public int? EpisodeId { get; set; }
        public int? SeasonId { get; set; }
        public string EpisodeTitle { get; set; }
    }
}