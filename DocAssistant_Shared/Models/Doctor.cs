using System.Collections.Generic;
using DocAssistant_Common.Attributes;

namespace DocAssistant_Common.Models
{
    public class Doctor : Staff
    {
        [Fixed] public List<Patient> Patients { get; set; } = new List<Patient>();
        [Fixed] public List<Assistant> Assistants { get; set; } = new List<Assistant>();
    }
}