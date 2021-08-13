using System.Collections.Generic;
using ShitLeopard.Api.Models;

namespace ShitLeopard.Common.Models
{
    public class EpisodeGroupingModel
    {
        public string Season { get; set; }
        public string SeasonId { get; set; }

        public string ShowName { get; set; }

    
        public List<EpisodeModel> Episodes { get; set; }
    }

    public class EpisodeGroupingViewModel
    {
        public List<EpisodeGroupingModel> Episodes { get; set; }
        public string ShowName { get; set; }

        public int SeasonCount { get; set; }
        public int EpisodeCount { get; set; }
    }
}