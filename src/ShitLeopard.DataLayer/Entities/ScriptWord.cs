﻿using System;
using System.Collections.Generic;

namespace ShitLeopard.DataLayer.Entities
{
    public partial class ScriptWord
    {
        public long Id { get; set; }
        public long ScriptLineId { get; set; }
        public string Word { get; set; }

        public virtual ScriptLine ScriptLine { get; set; }
    }
}
