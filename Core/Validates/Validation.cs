using Core.Concrets;
using Core.Extensions;
using System.Numerics;

namespace Core.Validates
{
    public class Validations
    {
        public static bool IsValidPin(string pin) => pin.Length == 7;

        public static bool IsValidSeriaNumber(string seriaNumber) => seriaNumber.ToLower().Contains("aa");
    }
}
