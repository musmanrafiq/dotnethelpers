using System;
using System.Linq;
using UHelpers.Enums;

namespace UHelpers
{
    public static class Strings
    {

        public static string RandomString(int length, StringType stringType = StringType.AlphabetsOnly)
        {
            var random = new Random();
            var chars = GetCharsList(stringType);
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static string GetCharsList(StringType stringType)
        {
            var chars = stringType switch
            {
                StringType.UperCaseAlphabetsOnly => "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                StringType.LowerCaseAlphabetsOnly => "abcdefghijklmnopqrstuvwxyz",
                StringType.AlphabetsOnly => "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz",
                StringType.UperCaseAlphaAndNumeric => "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789",
                StringType.LowerCaseAlphaAndNumeric => "abcdefghijklmnopqrstuvwxyz0123456789",
                StringType.AlphaAndNumeric => "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789",
                StringType.AlphaNumericWithSpecialCharacters => "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789",
                _ => throw new NotImplementedException()
            };
            return chars;
        }
    }
}
