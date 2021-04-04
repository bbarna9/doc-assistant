using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace DocAssistant_Common.Attributes
{
    public class CityValidationAttribute : ModelValidationAttribute
    {
        public CityValidationAttribute(int minLength, int maxLength, char [] invalidCharacters) : base(minLength,maxLength,invalidCharacters) {}
        
        public override bool IsValid([NotNull] object value)
        {
            var city = (string) value;

            if (city.Length < MinLength || city.Length > MaxLength) return false;

            foreach (var invalidChar in InvalidCharacters)
                if (city.Contains(invalidChar)) return false;
            
            return true;
        }
    }
}