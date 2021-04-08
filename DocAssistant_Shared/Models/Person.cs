using System.ComponentModel.DataAnnotations;
using DocAssistant_Common.Attributes;

namespace DocAssistant_Common.Models
{
    public abstract class Person
    {
        [Fixed][Key] public long Id { get; set; }
        [CommonValidation("^[^~!@#$%^&*\\(\\)-=_+\\{\\}\\[\\];\\\":<>\\/?,.|\\\\]{1,50}$")] public virtual string FirstName { get; set; }
        [CommonValidation("^[^~!@#$%^&*\\(\\)-=_+\\{\\}\\[\\];\\\":<>\\/?,.|\\\\]{1,50}$")] public virtual string LastName { get; set; }

        public override string ToString()
        {
            return $"[Id={Id};FirstName={FirstName};LastName={LastName}]";
        }
    }
}