using API.Dtos;
using API.DTOS;
using API.Models;
using AutoMapper;
namespace API.Mapper

{
    public class PersonProfile: Profile
    {
        public PersonProfile()
        {
            CreateMap<PersonDto, Person>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString())) // Genera un nuevo Id
              .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address != null
                  ? new Address
                  {
                      Id = Guid.NewGuid().ToString(),
                      AddressText = src.Address.AddressText,
                      Latitude = src.Address.Latitude,
                      Longitude = src.Address.Longitude
                  }
                  : null))
              .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => src.Address != null ? Guid.NewGuid().ToString() : null));

            // Mapeo de Person a PersonDto (Incluye AddressDto)
            CreateMap<Person, PersonDto>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address != null
                    ? new AddressDto
                    {
                        Id = src.Address.Id,
                        AddressText = src.Address.AddressText,
                        Latitude = src.Address.Latitude,
                        Longitude = src.Address.Longitude
                    }
                    : null));
        }
}
}
