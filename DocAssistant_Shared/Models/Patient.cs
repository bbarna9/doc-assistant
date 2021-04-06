using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DocAssistant_Common.Attributes;

namespace DocAssistant_Common.Models
{
    public sealed class Patient
    {
        public long Id { get; set; }
        
        [ForeignKey("DoctorId")] public long DoctorId { get; set; }

        [Required] 
        [CommonValidation("^[^~!@#$%^&*\\(\\)-=_+\\{\\}\\[\\];\\\":<>\\/?,.|\\\\]{1,50}$")] 
        public string FirstName { get; set; }
        [Required] 
        [CommonValidation("^[^~!@#$%^&*\\(\\)-=_+\\{\\}\\[\\];\\\":<>\\/?,.|\\\\]{1,50}$")] 
        public string LastName { get; set; }
      //  [Required] [AddressValidation] public Address Address { get; set; }
      
        [Required]
        [CountryValidation] 
        public string Country { get; set; }
        [Required]
        [StateValidation(minLength: 1, maxLength: 255, invalidCharacters: new char[] {'~','!','@','#','$','%','^','&','*','(',')','_','=','+','{','}',';','\"','<','>','?','\\','/','.','|'})] 
        public string State { get; set; }
        [Required]
        [CityValidation(minLength:1, maxLength: 255, invalidCharacters: new char[] {'~','!','@','#','$','%','^','&','*','(',')','_','=','+','{','}',';','\"','<','>','?','\\','/','.','|'} )] 
        public string City { get; set; }
        [Required]
        [CommonValidation("^.{1,250}$")]
        public string Street { get; set; }
        [Required]
        [ZIPValidation(minLength: 1, maxLength: 10)] 
        public string ZIP { get; set; }

        [Required] [SSNValidation] public string SSN { get; set; }
        
        [Required] public DateTime DateOfBirth { get; set; }

        public Patient()
        {
           
        }
        
    }
}