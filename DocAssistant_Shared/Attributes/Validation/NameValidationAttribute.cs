using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DocAssistant_Common.Attributes
{
    public class NameValidationAttribute : ValidationAttribute
    {
        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        public NameValidationAttribute(int minLength, int maxLength)
        {
            this.MinLength = minLength;
            this.MaxLength = maxLength;
        }
        public override bool IsValid([NotNull] object value)
        {
            return base.IsValid(value);
        }
    }
}