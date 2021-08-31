using System;
using System.Collections.Generic;

namespace ShitLeopard.Common.Models
{
    public class TrackedQueryModel
    {
 
        public string Ipaddress { get; set; }
        public Dictionary<string, object> Headers { get; set; }
        public string AgentString { get; set; }

        public string Route { get; set; }

        public string Query { get; set; }
        public DateTime? LastAccessTime { get; set; }
    }
}