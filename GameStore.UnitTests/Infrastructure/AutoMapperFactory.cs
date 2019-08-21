using AutoMapper;
using GameStore.Application.Infrastructure.Mapping;

namespace GameStore.UnitTests.Infrastructure
{
    public static class AutoMapperFactory
    {
        public static IMapper Create()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            return mappingConfig.CreateMapper();
        }
    }
}
