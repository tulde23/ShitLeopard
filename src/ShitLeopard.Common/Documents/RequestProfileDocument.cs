using System;
using System.Collections.Generic;
using System.Text;


namespace ShitLeopard.Common.Documents
{
    public class RequestProfileDocument : ElasticDocument
    {
        public object Headers { get; set; }

        public string Ipaddress { get; set; }

        public string AgentString { get; set; }

        public string Body { get; set; }
        public string Route { get; set; }
        public DateTime? LastAccessTime { get; set; }
    }
}
