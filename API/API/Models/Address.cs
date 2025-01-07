using API.Generics;

namespace API.Models
{
    public class Address: GenericEntity<string>
    {

        public double? latitude { get; set; }

        public double? longitude { get; set; }

        public string? AddressText { get; set; }
        public Person? Person { get; set; }
    }
}
