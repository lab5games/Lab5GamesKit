using System.Text.RegularExpressions;

namespace Lab5Games
{
    public static class StringExtensions
    {
        public static string FirstCharToUpper(this string input)
        {
            if (input == null)
                return "";

            if (input.Length == 1)
                return input.ToUpper();
                

            return char.ToUpper(input[0]) + input.Substring(1);
        }

        public static bool IsUrl(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                string v_pattern = @"((ht|f)tp(s?)\:\/\/)?www[.][0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?";
                if (Regex.IsMatch(str, v_pattern))
                    return true;
            }
            return false;
        }
    }
}
