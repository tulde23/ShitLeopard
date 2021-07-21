using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MongoDB.Entities;

namespace ShitLeopard.Common.Documents
{
    [Display(Name = "shows")]

    public partial class ShowDocument : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
