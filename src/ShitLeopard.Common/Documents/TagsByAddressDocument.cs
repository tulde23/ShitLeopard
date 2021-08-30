using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MongoDB.Entities;

namespace ShitLeopard.Common.Documents
{
    [Display(Name = "tagsByAddress")]
    public class TagsByAddressDocument : Entity
    {
        public string TagId { get; set; }
        public List<string> IPAddresses { get; set; }
        public TagsByAddressDocument()
        {
        }
    }
}
