using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DocAssistant_Common.Models;

namespace DocAssistantWebApi.Database.DbModels
{
    public abstract class Person
    {
        [Key] public long Id { get; set; }

        #region Name

        public string FirstName { get; set; }
        public string LastName { get; set; }

        #endregion

        #region Address

        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZIP { get; set; }

        #endregion
        public DateTime DateOfBirth { get; set; }
        
    }
}