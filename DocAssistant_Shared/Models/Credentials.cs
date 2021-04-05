using System.ComponentModel.DataAnnotations;

namespace DocAssistant_Common.Models
{
    public sealed class Credentials
    {
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
    }
}