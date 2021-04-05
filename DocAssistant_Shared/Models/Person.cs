namespace DocAssistant_Common.Models
{
    public abstract class Person
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}