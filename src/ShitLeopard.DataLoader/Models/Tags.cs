using System;
using System.Collections.Generic;

namespace ShitLeopard.DataLoader.Models
{
    public partial class Tags
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public long Frequency { get; set; }
    }
}
