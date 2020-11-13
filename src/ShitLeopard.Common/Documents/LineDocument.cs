using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Entities;

namespace ShitLeopard.Common.Documents
{
    [Display(Name = "lines")]
    public class LineDocument : IEntity
    {
        public string ID { get; set; }

        public long ScriptLineId { get; set; }
        public string Body { get; set; }
        public string CharacterId { get; set; }

        public long EpisodeId{get;set;}

        public string EpisodeTitle{ get; set; }

        public virtual CharacterDocument Character { get; set; }

        public void SetNewID()
        {
            ID = Guid.NewGuid().ToString();
        }
    }
}