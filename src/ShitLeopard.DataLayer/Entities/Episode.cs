﻿using System;
using System.Collections.Generic;

namespace ShitLeopard.DataLayer.Entities
{
    public partial class Episode
    {
        public Episode()
        {
            Script = new HashSet<Script>();
        }

        public long Id { get; set; }
        public int? OffsetId { get; set; }
        public string Title { get; set; }
        public long? SeasonId { get; set; }
        public string Synopsis { get; set; }

        public virtual Season Season { get; set; }
        public virtual ICollection<Script> Script { get; set; }
    }
}
