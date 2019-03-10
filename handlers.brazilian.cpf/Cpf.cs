using System;
using System.Globalization;
using System.Linq;

namespace handlers.brazilian
{
    public static partial class Cpf
    {
        public static bool IsValid(string cpf)
        {
            bool returnValue = false;

            try
            {
                Check(cpf);
                returnValue = true;
            }
            catch (Exception)
            {
                // return false for any error
                returnValue = false;
            }

            return returnValue;
        }

        public static void Check(string cpf)
        {
            string digits = new string(cpf.Where(char.IsDigit).ToArray());

            if (!Constants.CPF_LENGTH_WITHOUT_FORMATTING.Equals(digits.Length))
            {
                throw new FormatException(message: "Invalid argument length [" + digits.Length + "].");
            }

            if (HasRepeatedDigits(digits))
            {
                throw new ArgumentException(message: "Invalid CPF number.");
            }

            // verifies
            string verifiers = digits.Substring(9, 2);
            // to be verified
            string cpfNumbers = digits.Substring(0, 9);

            if (!verifiers[0].Equals(Mod11(cpfNumbers)))
            {
                throw new ArgumentException(message: "Invalid CPF number.");
            }

            cpfNumbers += verifiers[0];

            if (!verifiers[1].Equals(Mod11(cpfNumbers)))
            {
                throw new ArgumentException(message: "Invalid CPF number.");
            }
        }

        public static string Format(string cpf, bool check)
        {
            Check(cpf);
            return Format(cpf);
        }

        public static string Format(string cpf)
        {
            if (cpf == null)
            {
                throw new ArgumentNullException("Value cannot be null.");
            }

            if (string.Empty.Equals(cpf))
            {
                throw new ArgumentOutOfRangeException("Value cannot be empty.");
            }

            string digits = new string(cpf.Where(char.IsDigit).ToArray()).PadLeft(Constants.CPF_LENGTH_WITHOUT_FORMATTING, '0');

            return string.Format(Constants.CPF_MASK, digits[0], digits[1], digits[2], digits[3], digits[4], digits[5], digits[6], digits[7], digits[8], digits[9], digits[10]);
        }

        private static char Mod11(string number)
        {
            long returnValue = 0;

            for (int i = number.Length - 1, multiplier = 2; i >= 0; --i, ++multiplier)
                returnValue += int.Parse(number[i].ToString(), CultureInfo.InvariantCulture) * multiplier;

            var mod11 = returnValue % 11;
            return mod11 < 2 ? '0' : (11 - mod11).ToString(CultureInfo.InvariantCulture)[0];
        }

        private static bool HasRepeatedDigits(string cpf)
        {
            string[] invalidNumbers =
            {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999"
            };

            return invalidNumbers.Contains(cpf);
        }
    }
}
