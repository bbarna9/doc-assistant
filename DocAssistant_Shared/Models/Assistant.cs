using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocAssistant_Common.Models
{
    public class Assistant : Person
    {
        [Required]
        [ForeignKey("DoctorId")] public long DoctorId { get; set; }
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
    }
}