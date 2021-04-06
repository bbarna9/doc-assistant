using System.Collections.Generic;

namespace DocAssistant_Common.Models
{
    public class Doctor : Person
    {
        public string Username { get; set; }
        public string Password { get; set; }
        
        public List<Patient> Patients { get; set; } = new List<Patient>();
    }
}