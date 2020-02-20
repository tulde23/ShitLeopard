using System.Collections.Generic;

namespace ShitLeopard.Api.Models
{
    public class EpisodeModel
    {
        public long Id { get; set; }
        public int? OffsetId { get; set; }
        public string Title { get; set; }
        public long? SeasonId { get; set; }
        public string Synopsis { get; set; }
        public virtual SeasonModel Season { get; set; }
        public virtual List<ScriptModel> Script { get; set; }

      
    }
}