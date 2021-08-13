using System.Collections.Generic;
using ShitLeopard.Common.Models;

namespace ShitLeopard.Api.Models
{
    public class EpisodeModel
    {
        public string Id { get; set; }
        public int? OffsetId { get; set; }
        public string Title { get; set; }
        public string SeasonId { get; set; }
        public string Synopsis { get; set; }

        public string Body { get; set; }

        public ShowModel Show { get; set; }


      
    }
}