using Core.Concrets;
using Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils
{
    public class GetUserInput
    {
        public static string GetInput(string inputType, Predicate<string>? validate = null)
        {
            validate ??= input => !string.IsNullOrWhiteSpace(input);

            string? input;
            do
            {
                Messages.InputMessages(inputType);
                input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input) || !validate(input))
                    Messages.InvalidInput();

            } while (string.IsNullOrWhiteSpace(input) || !validate(input));

            return input;
        }

        public static T GetNumber<T>(string input = "choice") where T : INumber<T>
        {
            bool isSucceded;
            T? number;
            do
            {
                Messages.InputMessages(input);
                isSucceded = T.TryParse(Console.ReadLine(), null, out number);

                if (!isSucceded)
                    Messages.InvalidInput();

            } while (!isSucceded || number is null);

            return number;
        }

        public static char GetOpinion(string title, string action)
        {
        OpinionLabel: Messages.Opinion(title, action);
            string? input = Console.ReadLine();
            bool isSucceded = char.TryParse(input, out char choice);

            if (string.IsNullOrWhiteSpace(input) || !isSucceded || !input.IsValidChoice())
            {
                Messages.InvalidInput();
                goto OpinionLabel;
            }

            return choice;
        }
    }
}
