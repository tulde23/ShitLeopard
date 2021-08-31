using System;
using System.Collections.Generic;
using System.Text;


namespace ShitLeopard.Common.Documents
{
    public class TrackedQueryDocument : ElasticDocument
    {
        public Dictionary<string,object> Headers { get; set; }

        public string Ipaddress { get; set; }

        public string AgentString { get; set; }

        public string Query { get; set; }
        public string Route { get; set; }
        public DateTime? LastAccessTime { get; set; }
    }
}
