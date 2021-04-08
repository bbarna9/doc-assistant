using System.ComponentModel.DataAnnotations;
using DocAssistant_Common.Attributes;

namespace DocAssistant_Common.Models
{
    public abstract class Staff : Person
    {
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
    }
}