using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShitLeopard.Api.Models
{
    public class ScriptModel
    {
        public ScriptModel()
        {
        }

        public long Id { get; set; }
        public long EpisodeId { get; set; }
        public string Body { get; set; }

        public virtual EpisodeModel Episode { get; set; }
        public virtual List<ScriptLineModel> ScriptLine { get; set; }
    }
}
