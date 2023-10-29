using System;
using System.Text.RegularExpressions;

namespace CodeX.Core.Extensions
{
    public class CzTransformer
    {
        public class Standard
        {
            public class RegExPattern
            {
                public const string NorthAmericanPhoneNumber = @"(\d{3})(\d{3})(\d{4})";
            }
            public class RegExReplacement
            {
                public const string NorthAmericanPhoneNumber = "($1) $2-$3";
            }
        }

        public static string FormatPhoneNumber(
            string phoneNumber,
            int phoneNumberDigits = 10,
            string pattern = Standard.RegExPattern.NorthAmericanPhoneNumber,
            string replacement = Standard.RegExReplacement.NorthAmericanPhoneNumber)
        {
            string result = phoneNumber;
            try
            {
                // Remove everything except of numbers
                Regex regexObj = new Regex(@"[^\d]");
                var tempValue = regexObj.Replace(phoneNumber, "");

                // Format numbers to phone string
                if (tempValue.Length == phoneNumberDigits)
                {
                    result = Regex.Replace(tempValue, pattern, replacement);
                }
            }
            catch { }

            return result;
        }
    }
}
