using API.Generics;

namespace API.Models
{
    public class Person: GenericEntity
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public Address? Address { get; set; }

        public string? AddressId { get; set; }
    }
}
