using API.Mapper;
using API.MapperProfiles;

namespace API.Utilities
{
    public static class Mapper
    {
        public static void InitializeMappings(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(PersonProfile));
            services.AddAutoMapper(typeof(AddressProfile));
        }
    }
}
