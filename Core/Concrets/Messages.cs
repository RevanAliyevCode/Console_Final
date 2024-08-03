using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Concrets
{
    public static class Messages
    {
        public static void InvalidInput() => Console.WriteLine("Please enter valid input");
        public static void ErrorOcured() => Console.WriteLine("Something went wrong");
        public static void InputMessages(string title) => Console.Write($"Write {title}: ");
        public static void SuccessMessage(string title, string action) => Console.WriteLine($"Successfully {action} {title}");
        public static void Exist(string title, string name) => Console.WriteLine($"{title} - {name} already exist");
        public static void NotFound(string title) => Console.WriteLine($"{title} not found");
        public static void Opinion(string title, string action) => Console.Write($"Do you want to {action} {title} (y/n): ");
        public static void InvalidChoice() => Console.WriteLine("There is no operation like that");
        public static void LoginError() => Console.WriteLine("email or password incorrect");
        public static void OutOfStock() => Console.WriteLine("This product out of stock");
    }
}
