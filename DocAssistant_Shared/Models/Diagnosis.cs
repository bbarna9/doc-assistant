using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DocAssistant_Common.Attributes;

namespace DocAssistant_Common.Models
{
    public sealed class Diagnosis
    {
        [Fixed] [ForeignKey("PatientId")] public long PatientId { get; set; }
        [Required] [CommonValidation("^.{1,35}$")] public string Title { get; set; }
        [Required] [CommonValidation("^.{1,500}$")] public string Description { get; set; }
        [Required] public DateTime Date { get; set; }
    }
}