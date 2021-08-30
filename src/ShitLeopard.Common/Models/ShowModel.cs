using System;
using System.Collections.Generic;
using System.Text;

namespace ShitLeopard.Common.Models
{
    public class ShowModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int SeasonCount{get;set;}

       public int EpisodeCount { get; set; }
    }
}
