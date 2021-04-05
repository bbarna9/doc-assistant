using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using PatientModel = DocAssistant_Common.Models.Patient;

namespace DocAssistantWebApi.Database.DbModels
{
    public class Patient : Person
    {
        public string SSN { get; set; }

        public static explicit operator Patient(PatientModel patient)
        {
            return new Patient
            {
                FirstName = patient.FirstName,
                LastName =  patient.LastName,
                SSN = patient.SSN,
                Country = patient.Address.Country, // ! 
                DateOfBirth = patient.DateOfBirth
            };
        }

        public static implicit operator PatientModel(Patient patient)
        {
            return new PatientModel
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                SSN = patient.SSN,
                DateOfBirth = patient.DateOfBirth
                
            };
        }
    }
}