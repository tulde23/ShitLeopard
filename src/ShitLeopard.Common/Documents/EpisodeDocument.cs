
using System.ComponentModel.DataAnnotations;

namespace ShitLeopard.Common.Documents
{
    [Display(Name ="episodes")]
    public class EpisodeDocument
    {
        public EpisodeDocument()
        {
        }

        public long Id { get; set; }
        public int? OffsetId { get; set; }
        public string Title { get; set; }
        public long? SeasonId { get; set; }
        public string Synopsis { get; set; }
    }
}