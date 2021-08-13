using System;

namespace ShitLeopard.DataLoader
{
    public static class Title
    {
        private const string _logo = @"

 .-._                                                   _,-,
  `._`-._                                           _,-'_,'
     `._ `-._                                   _,-' _,'
        `._  `-._        __.-----.__        _,-'  _,'
           `._   `#==='''           '''===#'   _,'
              `._/)  ._ _.  (\_,'
               )*'     **.__     __.**     '*(
#  .==..__  ""   ""  __..==,  #
               #   `'._(_).       .(_)_.''   #

";

        public static void Write()
        {
            var title = "ShitLeopard Data Loader - Pussy Tits.";
            int width = Console.WindowWidth;
            var whitespace = width - title.Length - 2;
            //ServiceIdentity serviceIdentity, Uri consulUri, TimeSpan cacheExpiration, string datacenter = nul

            Console.WriteLine("=".Replicate(width));
            Console.Write("#");
            Console.Write(" ".Replicate(whitespace / 2));
            Console.Write(title);
            Console.Write(" ".Replicate(whitespace / 2));
            Console.WriteLine("=");
            Console.WriteLine("#".Replicate(width));
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(_logo);

            Console.ResetColor();
        }
    }
}