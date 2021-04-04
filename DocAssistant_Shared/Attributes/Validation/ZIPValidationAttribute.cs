using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DocAssistant_Common.Attributes
{
    public class ZIPValidationAttribute : ModelValidationAttribute
    {
        public ZIPValidationAttribute(int minLength, int maxLength) : base(minLength,maxLength) {}
        
        public override bool IsValid([NotNull] object value)
        {
            var zip = (string) value;

            return zip.Length <= this.MaxLength && zip.Length >= this.MinLength;
        }
    }
}