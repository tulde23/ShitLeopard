using System.ComponentModel.DataAnnotations;
using MongoDB.Entities;

namespace ShitLeopard.Common.Documents
{
    [Display(Name = "characters")]
    public partial class CharacterDocument : Entity
    {
        public CharacterDocument()
        {
        }

        public string Name { get; set; }
        public string Aliases { get; set; }
        public string Notes { get; set; }
        public string PlayedBy { get; set; }
    }
}