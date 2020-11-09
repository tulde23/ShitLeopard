using System;
using System.Collections.Generic;

namespace ShitLeopard.DataLayer.Entities
{
    public partial class Quote
    {
        public long Id { get; set; }
        public long? CharacterId { get; set; }
        public long? ScriptLineId { get; set; }
        public string Body { get; set; }
        public int Popularity { get; set; }
    }
}
