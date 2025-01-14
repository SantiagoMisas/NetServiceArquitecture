using API.Dtos;
using API.Models;
using AutoMapper;

namespace API.MapperProfiles
{
    public class AddressProfile: Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
