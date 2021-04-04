using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace DocAssistant_Common.Attributes
{
    public class CountryValidationAttribute : ValidationAttribute
    {
        private readonly Regex countryRegex = new Regex("^[a-zA-Z ]+$");
        
        public override bool IsValid([NotNull] object value)
        {
            string country = (string) value;

            return countryRegex.IsMatch(country);
        }
    }
}