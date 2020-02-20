using System.Linq;

namespace System
{
    public static class StringExtensions
    {
        public static string Replicate(this string input, int length)
        {
            return string.Join(string.Empty, Enumerable.Range(0, length).Select(x => input));
        }
    }
}