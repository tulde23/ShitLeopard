using System;

namespace ShitLeopard.Common.Models
{
    public class RequestProfileModel
    {
        public string Ipaddress { get; set; }
        public string Headers { get; set; }
        public string AgentString { get; set; }

        public string Route { get; set; }
        public DateTime? LastAccessTime { get; set; }
    }
}