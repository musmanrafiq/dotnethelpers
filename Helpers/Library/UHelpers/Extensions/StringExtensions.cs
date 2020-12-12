using System;
using System.Linq;
using System.Text;

namespace UHelpers.Extensions
{
    public static class StringExtentions
    {
        public static string ToSnakeCase(this string str)
        {
            return string.Concat(
                str.Select(
                    (x, i) => i > 0 && char.IsUpper(x) ? $"_{x}" : x.ToString())
                ).ToLower();
        }
        public static string EncodeToBase64(this string plainTxt)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainTxt);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string DecodeBase64(this string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
