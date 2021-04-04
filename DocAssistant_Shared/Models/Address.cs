using DocAssistant_Common.Attributes;

namespace DocAssistant_Common.Models
{
    public struct Address
    {
        [CountryValidation] 
        public string Country { get; set; }
        [StateValidation(minLength: 1, maxLength: 255, invalidCharacters: new char[] {'~','!','@','#','$','%','^','&','*','(',')','_','=','+','{','}',';','\"','<','>','?','\\','/','.','|'})] 
        public string State { get; set; }
        [CityValidation(minLength:1, maxLength: 255, invalidCharacters: new char[] {'~','!','@','#','$','%','^','&','*','(',')','_','=','+','{','}',';','\"','<','>','?','\\','/','.','|'} )] 
        public string City { get; set; }
        [StreetValidation(minLength: 1, maxLength: 255)] 
        public string Street { get; set; }
        [ZIPValidation(minLength: 1, maxLength: 10)] 
        public string ZIP { get; set; }

        public Address(string country, string state, string city, string street, string zip)
        {
            this.Country = country;
            this.State = state;
            this.City = city;
            this.Street = street;
            this.ZIP = zip;
        }
    }
}