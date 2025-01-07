using API.Dtos;
using API.DTOS;

namespace API.DTOS
{
    public class PersonDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public AddressDto? Address { get; set; }

        public string? AddressId { get; set; }
    }
}
