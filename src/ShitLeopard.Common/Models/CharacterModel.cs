using System.Collections.Generic;

namespace ShitLeopard.Api.Models
{
    public class CharacterModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Aliases { get; set; }
        public string Notes { get; set; }
        public string PlayedBy { get; set; }

        public virtual List<ScriptLineModel> ScriptLine { get; set; }
    }
}