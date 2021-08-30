using System.ComponentModel.DataAnnotations;


namespace ShitLeopard.Common.Documents
{
    [Display(Name = "characters")]
    public partial class CharacterDocument : ElasticDocument
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