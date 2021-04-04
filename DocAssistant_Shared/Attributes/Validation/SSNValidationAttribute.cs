using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;

namespace DocAssistant_Common.Attributes
{
    public class SSNValidationAttribute : ValidationAttribute
    {
        /*public override bool IsValid([NotNull] object value)
        {
            ushort[] taj = (ushort[]) value;
            
            if (taj.Length != 9) return false;
            
            foreach (var number in taj)
                if (number > 9) return false;

            return true;
        }*/
        
        private readonly Regex allowedPattern = new Regex("^[0-9-]+$");

        public override bool IsValid([NotNull] object value)
        {
            // Default: 000-000-000 -> length: 11
            
            StringBuilder ssn = (StringBuilder) value;

            if (ssn.Length != 11) return false;

            foreach (var character in ssn.GetChunks())
            {
                if (!allowedPattern.IsMatch(character.ToString())) return false;
            }
            
            return true;
        }
    }
}