using System.Collections.Generic;

namespace ShitLeopard.Api.Models
{
    public class EpisodeModel
    {
        public string Id { get; set; }
        public int? OffsetId { get; set; }
        public string Title { get; set; }
        public string SeasonId { get; set; }
        public string Synopsis { get; set; }


      
    }
}