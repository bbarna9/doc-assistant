using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DocAssistant_Common.Attributes
{
    public class StateValidationAttribute : ModelValidationAttribute
    {
        
        public StateValidationAttribute(int minLength, int maxLength, char [] invalidCharacters) : base(minLength,maxLength,invalidCharacters) {}
        
        public override bool IsValid([NotNull] object value)
        {
            var state = (string) value;

            if (state.Length < MinLength || state.Length > MaxLength) return false;
            
            foreach (var invalidChar in InvalidCharacters)
                if (state.Contains(invalidChar)) return false;

            return true;
        }
    }
}