using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CodeX.Core.Models
{
    public sealed class SpecialCharacterValidationAttribute : ValidationAttribute
    {
        public SpecialCharacterValidationAttribute(string exclusionCharacterSet)
        {
            CharacterSet = exclusionCharacterSet.ToCharArray();
        }

        public Char[] CharacterSet { get; }

        public override bool IsValid(object value)
        {
            var result = false;
            try
            {
                var stringValue = Convert.ToString(value);
                var hasCharacterSet = stringValue.ToCharArray();
                var badCharacterSet = hasCharacterSet.Intersect(CharacterSet);
                result = (!badCharacterSet.Any());
            }
            catch { }
            return result;
        }
    }
}