using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Entities;

namespace ShitLeopard.Common.Documents
{
    [Display(Name = "tags")]
    public class TagsDocument : IEntity
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }

        public long Frequency { get; set; }

        public void SetNewID()
        {
            if (string.IsNullOrEmpty(ID))
            {
                ID = Guid.NewGuid().ToString();
            }
        }
    }
}