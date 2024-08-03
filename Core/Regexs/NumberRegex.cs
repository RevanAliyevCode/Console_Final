using System.Text.RegularExpressions;

namespace Core.Regexs
{
    public class NumberRegex
    {
        public static bool IsValidNumber(string number)
        {
            Regex regex = new(@"^\+994\d{9}$");
            return regex.IsMatch(number);
        }
    }
}
