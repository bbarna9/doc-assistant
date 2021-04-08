using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DocAssistant_Common.Attributes;

namespace DocAssistant_Common.Models
{
    public class Assistant : Staff
    {
        [Fixed]
        [Required]
        [ForeignKey("DoctorId")] public long DoctorId { get; set; }
    }
}