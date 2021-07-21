using System.Threading.Tasks;
using ShitLeopard.DataLoader.Contracts;

namespace ShitLeopard.DataLoader.Shows
{
    public abstract class AbstractBulkDataLoader : IShowBulkDataLoader
    {
        public AbstractBulkDataLoader(ConsoleApplication consoleApplication,
            Options options)
        {
            ConsoleApplication = consoleApplication;
            Options = options;
        }

        public ConsoleApplication ConsoleApplication { get; }
        public Options Options { get; }

        public abstract Task ImportAsync();
    }
}