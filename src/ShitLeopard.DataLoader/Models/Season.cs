using System;
using System.Collections.Generic;

namespace ShitLeopard.DataLayer.Entities
{
    public partial class Season
    {
        public Season()
        {
            Episode = new List<Episode>();
        }

        public long Id { get; set; }
        public string Title { get; set; }

        public virtual List<Episode> Episode { get; set; }
    }
}
