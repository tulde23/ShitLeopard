using System.IO;

namespace ShitLeopard.DataLoader
{
    public class Options
    {
        public string[] Arguments { get; }

        public DirectoryInfo DocumentDirectory { get; }

        public Options(params string[] args)
        {
            this.Arguments = args;
            if (args.Length == 1)
            {
                DocumentDirectory = new DirectoryInfo(args[0]);
            }
            else
            {
                DocumentDirectory = new DirectoryInfo(@"C:\Development\ShitLeopard\ClosedCaptions");
            }
        }
    }
}