using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DocAssistantWebApi.Database.DbModels
{
    public class Doctor : Person
    {
        [Key] public string Username { get; set; }
        public string Password { get; set; }
        
        public List<Patient> Patients { get; set; }
    }
}