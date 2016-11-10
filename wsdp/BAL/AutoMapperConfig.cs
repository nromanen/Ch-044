using AutoMapper;

namespace BAL
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(
                cfg => cfg.AddProfile(new MappingProfile())
                );
        }
    }
}