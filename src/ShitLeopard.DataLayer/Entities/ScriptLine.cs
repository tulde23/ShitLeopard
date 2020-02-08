using System;
using System.Collections.Generic;

namespace ShitLeopard.DataLayer.Entities
{
    public partial class ScriptLine
    {
        public ScriptLine()
        {
            ScriptWord = new HashSet<ScriptWord>();
        }

        public long Id { get; set; }
        public string Body { get; set; }
        public long ScriptId { get; set; }
        public long? CharacterId { get; set; }

        public virtual Character Character { get; set; }
        public virtual Script Script { get; set; }
        public virtual ICollection<ScriptWord> ScriptWord { get; set; }
    }
}
