using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DocAssistantWebApi.Database.DbModels
{
    public class Doctor : Person
    {
        public List<Patient> Patients { get; set; }
    }
}