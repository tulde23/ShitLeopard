using System;
using System.Collections.Generic;
using System.Text;

namespace ShitLeopard.DataLoader.Models
{
   public  class WikiScrapRequest
    {
        public WikiScrapRequest( string showName, int lastEpisodeId, string showId, IEnumerable<string> additionalClassNames)
        {
            ShowName = showName;
            LastEpisodeId = lastEpisodeId;
            ShowId = showId;
            AdditionalClassNames = additionalClassNames;
        }

        public string ShowName { get; }

        public string ShowId { get; set; }
        public int LastEpisodeId { get; }
        public IEnumerable<string> AdditionalClassNames { get; }
    }
    public class EastboundAndDownWikiScrapRequest : WikiScrapRequest
    {
        public EastboundAndDownWikiScrapRequest(): 
            base("List_of_Eastbound_%26_Down_episodes", 29, "60f6d94e10ab6bd56f5ec7c5", null)
        {
        }
    }
    public class TrailerParkBoysWikiScrapRequest : WikiScrapRequest
    {
        public   TrailerParkBoysWikiScrapRequest (): 

            base("List_of_Trailer_Park_Boys_episodes", 105, "60f6d90310ab6bd56f5ec7c4", null)
        {
        }
    }
}
