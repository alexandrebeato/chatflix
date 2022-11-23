using AutoMapper;

namespace API.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            if (services == null)
                return;

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Models.Users.Mapper.ModelToDomainMapping());
                mc.AddProfile(new Models.Users.Mapper.DomainToModelMapping());
                mc.AddProfile(new Models.Rooms.Mapper.ModelToDomainMapping());
                mc.AddProfile(new Models.Rooms.Mapper.DomainToModelMapping());
                mc.AddProfile(new Models.Messages.Mapper.DomainToModelMapping());
            });

            services.AddSingleton(mapperConfig.CreateMapper());
        }
    }
}