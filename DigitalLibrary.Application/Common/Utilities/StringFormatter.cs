
using System.Text.RegularExpressions;

namespace DigitalLibrary.Application.Common.Utilities
{
    public class StringFormatter
    {
        public static string RemovePunctuationAndNumbers(string input)
        {
            // Replace \n with space
            input = Regex.Replace(input, @"\n", " ");

            // Remove punctuation and special characters
            input = Regex.Replace(input, @"[^\w\s]", " ");

            //removes digits
            input = Regex.Replace(input, @"\b\w*\d\w*\b", "");

            //removes whitespaces
            string result = Regex.Replace(input, @"\s+", " ");

            return result.ToLower().Trim();
        }
    }
}