using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DocAssistant_Common.Attributes;

namespace DocAssistant_Common.Models
{
    public sealed class Patient
    {
        public long Id { get; set; }
        
        [NameValidation(minLength:1,maxLength:50)] public string FirstName { get; set; }
        [NameValidation(minLength:1,maxLength:50)] public string LastName { get; set; }
        [AddressValidation] public Address Address { get; set; }
        [SSNValidation] public StringBuilder SSN { get; set; }
        
        public DateTime DateOfBirth { get; set; }

        public Patient()
        {
           
        }
        
    }
}