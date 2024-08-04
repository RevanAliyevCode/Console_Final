using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Concrets
{
    public static class Messages
    {
        public static void InvalidInput()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please enter valid input");
            Console.ResetColor();
        }
        public static void ErrorOcured()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Something went wrong");
            Console.ResetColor();
        }
        public static void InputMessages(string title)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"Write {title}: ");
            Console.ResetColor();
        }
        public static void SuccessMessage(string title, string action)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Successfully {action} {title}");
            Console.ResetColor();
        }
        public static void Exist(string title, string name)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{title} - {name} already exist");
            Console.ResetColor();
        }

        public static void NotFound(string title)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{title} not found");
            Console.ResetColor();
        }

        public static void Opinion(string title, string action)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"Do you want to {action} {title} (y/n): ");
            Console.ResetColor();
        }

        public static void InvalidChoice()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("There is no operation like that");
            Console.ResetColor();
        }

        public static void LoginError()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("email or password incorrect");
            Console.ResetColor();
        }

        public static void OutOfStock()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("This product out of stock");
            Console.ResetColor();
        }
    }
}
