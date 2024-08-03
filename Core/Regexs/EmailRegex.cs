using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Core.Regexs
{
    public class EmailRegex
    {
        public static bool IsValidEmail(string email)
        {
            Regex regex = new(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(email);
        }
    }
}
