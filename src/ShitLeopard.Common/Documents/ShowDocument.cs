using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace ShitLeopard.Common.Documents
{
    [Display(Name = "shows")]

    public partial class ShowDocument : ElasticDocument
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int LastEpisodeId { get; set; }


    }
}
