using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DocAssistant_Common.Attributes;

namespace DocAssistant_Common.Models
{
    public sealed class Patient
    {
        public long Id { get; set; }
        
        [Required] [NameValidation(minLength:1,maxLength:50)] public string FirstName { get; set; }
        [Required] [NameValidation(minLength:1,maxLength:50)] public string LastName { get; set; }
        [Required] [AddressValidation] public Address Address { get; set; }
        [Required] [SSNValidation] public string SSN { get; set; }
        
        [Required] public DateTime DateOfBirth { get; set; }

        public Patient()
        {
           
        }
        
    }
}