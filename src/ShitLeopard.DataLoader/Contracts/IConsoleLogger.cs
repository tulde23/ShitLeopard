using System;

namespace ShitLeopard.DataLoader.Contracts
{
    public interface IConsoleLogger
    {
        void Write(object data, ConsoleColor color = ConsoleColor.Green);
    }

    public class ConsoleLogger : IConsoleLogger
    {
        private static readonly  object _syncroot = new object();

        public void Write(object data, ConsoleColor color = ConsoleColor.Green)
        {
            lock (_syncroot)
            {
                Console.ForegroundColor = color;
                Console.WriteLine($"{data}");
                Console.ResetColor();
            }
        }
    }
}