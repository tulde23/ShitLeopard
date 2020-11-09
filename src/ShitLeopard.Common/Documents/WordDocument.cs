using System.ComponentModel.DataAnnotations;

namespace ShitLeopard.Common.Documents
{
    [Display(Name = "words")]
    public class WordDocument
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public long ScriptLineId { get; set; }
    }
}