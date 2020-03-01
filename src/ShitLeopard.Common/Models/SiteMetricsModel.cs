using System;
using System.Collections.Generic;

namespace ShitLeopard.Common.Models
{
    public class SiteMetricsModel
    {
        public object Headers { get; set; }

        public string Ipaddress { get; set; }

        public string AgentString { get; set; }

        public string Route { get; set; }
        public DateTime? LastAccessTime { get; set; }
    }
}