using CommandLine;

namespace ShitLeopard.DataLoader
{
    public class Options
    {
        [Option('a', "action", Required = true, HelpText = "action to run")]
        public string Action { get; set; }
        [Option('i', "import", Required = false, HelpText = "Import Directory")]
        public string ImportDirectory { get; set; }
    }
}