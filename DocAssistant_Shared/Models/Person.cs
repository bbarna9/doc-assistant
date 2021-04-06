using System.ComponentModel.DataAnnotations;

namespace DocAssistant_Common.Models
{
    public abstract class Person
    {
        [Key] public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override string ToString()
        {
            return $"[Id={Id};FirstName={FirstName};LastName={LastName}]";
        }
    }
}