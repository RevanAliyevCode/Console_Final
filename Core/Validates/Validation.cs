using Core.Concrets;
using Core.Extensions;
using System.Numerics;

namespace Core.Validates
{
    public class Validation
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

        public static bool IsValidPin(string pin) => pin.Length == 7;

        public static bool IsValidSeriaNumber(string seriaNumber) => seriaNumber.ToLower().Contains("aa");
    }
}
