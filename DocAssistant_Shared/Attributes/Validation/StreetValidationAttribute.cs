using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DocAssistant_Common.Attributes
{
    public class StreetValidationAttribute : ModelValidationAttribute
    {
        public StreetValidationAttribute(int minLength, int maxLength) : base(minLength,maxLength) {}
        
        public override bool IsValid([NotNull] object value)
        {
            return base.IsValid(value);
        }
    }
}