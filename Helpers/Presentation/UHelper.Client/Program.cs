using System;
using UHelpers;

namespace UHelper.Client
{
    static class Program
    {
        static void Main(string[] args)
        {
            var tempString = Strings.RandomString(10);
            Console.WriteLine(tempString);
            Console.ReadKey();
        }
    }
}
