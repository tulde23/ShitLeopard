using System;
using System.Collections.Generic;

namespace ShitLeopard.DataLoader.Models
{
    public partial class Season
    {
        public Season()
        {
            Episode = new List<Episode>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual List<Episode> Episode { get; set; }


        
    }
}
