using System;
using System.Collections.Generic;

namespace ShitLeopard.DataLoader.Models
{
    public partial class CharacterTags
    {
        public long Id { get; set; }
        public long CharacterId { get; set; }
        public long TagId { get; set; }
    }
}
