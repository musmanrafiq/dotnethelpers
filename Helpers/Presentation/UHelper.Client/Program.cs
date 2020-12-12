using System;
using UHelpers.Extensions;

namespace UHelper.Client
{
    static class Program
    {
        private static void Main(string[] args)
        {
            var tempString = "HowAreYou";
            var sn = tempString.ToSnakeCase();
            Console.WriteLine(sn);
            Console.ReadKey();
        }
    }
}
