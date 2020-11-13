using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Entities;

namespace ShitLeopard.Common.Documents
{
    public class RequestProfileDocument : Entity
    {
        public object Headers { get; set; }

        public string Ipaddress { get; set; }

        public string AgentString { get; set; }

        public string Route { get; set; }
        public DateTime? LastAccessTime { get; set; }
    }
}
