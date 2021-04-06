using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using DocAssistant_Common.Attributes;

namespace DocAssistant_Common.Models
{
    public sealed class Credentials
    {
        [Required] 
        [CommonValidation(pattern: "(^[a-zA-Z0-9]{1}([a-zA-Z0-9-_]{2,48})[a-zA-Z0-9]{1}$)")] // Invalid: `-username`, `username-` | Valid: `username`, `us_er-name`
        public string Username { get; set; }
        [Required]
        [CommonValidation("^.{6,125}$")]
        public string Password { get; set; }
    }
}