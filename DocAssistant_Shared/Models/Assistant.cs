using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocAssistant_Common.Models
{
    public class Assistant : Person
    {
        [Required]
        [ForeignKey("DoctorId")] public long DoctorId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}