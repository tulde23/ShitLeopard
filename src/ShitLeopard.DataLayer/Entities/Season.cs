using System;
using System.Collections.Generic;

namespace ShitLeopard.DataLayer.Entities
{
    public partial class Season
    {
        public Season()
        {
            Episode = new HashSet<Episode>();
        }

        public long Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Episode> Episode { get; set; }
    }
}
