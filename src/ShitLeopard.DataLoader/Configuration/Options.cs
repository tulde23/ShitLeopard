using CommandLine;

namespace ShitLeopard.DataLoader
{
    public class Options
    {
        [Option('d', "data", Required = true, HelpText = "location of the configuration data.")]
        public string DataDirectory { get; set; }
 

        [Option('s', "show", Required = true, HelpText = "trailerParkBoys | eastboundAndDown | all")]
        public string ShowName { get; set; }
        [Option('x', "drop", Required = false, HelpText = "drop database")]
        public bool DropCollections { get; set; }
    }
}