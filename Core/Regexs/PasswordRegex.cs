using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Core.Regexs
{
    public class PasswordRegex
    {
        public static bool IsValidPassword(string password)
        {
            Regex regex = new(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");
            return regex.IsMatch(password);
        }
    }
}
