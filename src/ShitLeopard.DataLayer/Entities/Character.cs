using System;
using System.Collections.Generic;

namespace ShitLeopard.DataLayer.Entities
{
    public partial class Character
    {
        public Character()
        {
            ScriptLine = new HashSet<ScriptLine>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<ScriptLine> ScriptLine { get; set; }
    }
}
