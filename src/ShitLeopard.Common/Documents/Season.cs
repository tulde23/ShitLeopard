using System.ComponentModel.DataAnnotations;

namespace ShitLeopard.Common.Documents
{
    [Display(Name = "seasons")]
    public partial class SeasonDocument
    {
        public SeasonDocument()
        {
        }

        public long Id { get; set; }
        public string Title { get; set; }
    }
}