using System.Threading;

namespace ShitLeopard.DataLoader
{
    /// <summary>
    ///
    /// </summary>
    public class ConsoleApplication
    {
        /// <summary>
        /// Gets the token source.
        /// </summary>
        /// <value>
        /// The token source.
        /// </value>
        public CancellationTokenSource TokenSource { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleApplication"/> class.
        /// </summary>
        public ConsoleApplication()
        {
            TokenSource = new CancellationTokenSource();
        }
    }
}