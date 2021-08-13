using System.Collections.Generic;
using System.IO;

namespace ShitLeopard.DataLoader.Configuration
{
    public class ShowConfiguration
    {
        public string RootFolder { get; set; }

        public string FileName { get; set; }
        public string ShowName { get; set; }
        public string ClosedCaptionsPath { get; set; }

        public string ClosedCaptionsParser { get; set; }

        public string ClosedCaptionsFileExtension { get; set; }
        public MetadataProviderConfiguration Metadata { get; set; }


        public string GetCacheFileName()
        {
            return Path.Combine(RootFolder, "Cache", $"{ShowName.Replace(" ","")}.Entities.json");
        }
    }

    public class MetadataProviderConfiguration
    {
        public string Provider { get; set; }
        public List<string> ClassSelectors { get; set; }

        public string ShowName { get; set; }
        public string ShowId { get; set; }
        public int LastEpisodeId { get; set; }
    }
}