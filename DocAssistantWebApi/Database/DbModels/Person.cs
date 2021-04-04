using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DocAssistantWebApi.Database.DbModels
{
    public abstract class Person
    {
        [Key] public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        
    }
}