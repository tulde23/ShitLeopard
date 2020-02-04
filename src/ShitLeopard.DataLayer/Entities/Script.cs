using System.Collections.Generic;

namespace ShitLeopard.Entities
{
    public partial class Script
    {
        public Script()
        {
            ScriptLine = new HashSet<ScriptLine>();
        }

        public long Id { get; set; }
        public long EpisodeId { get; set; }
        public string Body { get; set; }

        public virtual Episode Episode { get; set; }
        public virtual ICollection<ScriptLine> ScriptLine { get; set; }
    }
}