using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShitLeopardBlazor.ViewModels
{
    public class SearchResult
    {
        public string id { get; set; }
        public int dialogLineNumber { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string body { get; set; }
        public int episodeNumber { get; set; }
        public int episodeOffsetId { get; set; }
        public string episodeTitle { get; set; }
        public string seasonId { get; set; }
        public string synopsis { get; set; }
        public string showName { get; set; }

    }
}
