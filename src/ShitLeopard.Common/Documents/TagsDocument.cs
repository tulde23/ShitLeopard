using System.ComponentModel.DataAnnotations;

namespace ShitLeopard.Common.Documents
{
    [Display(Name = "tags")]
    public class TagsDocument
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }

        public long Frequency { get; set; }
    }
}