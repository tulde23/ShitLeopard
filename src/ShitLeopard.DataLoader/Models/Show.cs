using System.Collections.Generic;
using ShitLeopard.DataLoader.Configuration;

namespace ShitLeopard.DataLoader.Models
{
    public class Show
    {
        public Show()
        {
        }

        public Show(ShowConfiguration showConfiguration)
        {
            ShowId = showConfiguration.Metadata.ShowId;
            Title = showConfiguration.ShowName;
            LastEpisodeId = showConfiguration.Metadata.LastEpisodeId;
        }

        public string ShowId { get; set; }
        public string Title { get; set; }

        public int LastEpisodeId { get; set; }

        public List<Season> Seasons { get; set; }
    }
}