using API.Generics;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class Address: GenericEntity
    {

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string? AddressText { get; set; }
        [JsonIgnore]
        public Person? Person { get; set; }
    }
}
