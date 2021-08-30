using System;
using System.Collections.Generic;

namespace ShitLeopard.DataLoader.Models
{
    public partial class RequestProfile
    {
        public long Id { get; set; }
        public string Ipaddress { get; set; }
        public string Headers { get; set; }
        public string AgentString { get; set; }

        public string Route { get; set; }
        public DateTime? LastAccessTime { get; set; }
    }
}
