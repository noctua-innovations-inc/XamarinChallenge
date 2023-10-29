using CodeX.Core.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CodeX.Core.Models
{
    public sealed class PasswordValidationAttribute : ValidationAttribute
    {
        public PasswordValidationAttribute(uint minimumLength)
        {
            MinimumLength = minimumLength;
        }

        public uint MinimumLength { get; }

        public uint MaximumLength { get; set; }

        public override bool IsValid(object value)
        {
            var stringValue = Convert.ToString(value);
            if (string.IsNullOrWhiteSpace(stringValue))
            {
                return false;
            }

            var len = stringValue.Length;
            if ((len < MinimumLength) || (len > MaximumLength))
            {
                Reason = $"Password must have from {MinimumLength} to {MaximumLength} characters";
                return false;
            }

            var characterArray = stringValue.ToCharArray();

            var hasUpperCase = characterArray.Any(c => c >= 'A' && c <= 'Z');
            if (!hasUpperCase)
            {
                Reason = "Password must have at least one uppercase letter";
                return false;
            }

            var hasLowerCase = characterArray.Any(c => c >= 'a' && c <= 'z');
            if (!hasLowerCase)
            {
                Reason = "Password must have at least one lowercase letter";
                return false;
            }

            if (HasRepetitiveSequence(stringValue))
            {
                Reason = "Password must not have repetitive character sequences";
                return false;
            }

            return true;
        }

        public string Reason { get; private set; }

        public override string FormatErrorMessage(string name)
        {
            return Reason;
        }

        public static bool HasRepetitiveSequence(string value)
        {
            if ((value?.Length ?? 0) <= 1)
            {
                return false;
            }

            // Odd length strings require a second test with an offset of 1
            for (var d = 0; d <= (value.Length % 2); d++)
            {
                var testValue = value.Substring(d);

                // Repetition can not be longer than its comparison
                for (var x = 0; x < (value.Length / 2); x++)
                {
                    var sections = value.Split(x + 1).ToList();
                    for (var y = 0; y < (sections.Count() - 1); y++)
                    {
                        // Check for repetition
                        if (sections[y] == sections[y + 1])
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}