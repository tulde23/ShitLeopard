using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ShitLeopard.Common.Documents
{
    [Display(Name = "tags")]
    public class TagsDocument : ElasticDocument
    {
        public string Name { get; set; }
        public string Category { get; set; }

        public long Frequency { get; set; }

        public List<string> IPAddresses { get; set; } = new List<string>();
    }
}