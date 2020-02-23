using System.Collections.Generic;
using ShitLeopard.Api.Models;

namespace ShitLeopard.Common.Models
{
    public class EpisodeGroupingModel
    {
        public string Season { get; set; }
        public long?  SeasonId { get; set; }
        public List<EpisodeModel> Episodes { get; set; }
    }
}