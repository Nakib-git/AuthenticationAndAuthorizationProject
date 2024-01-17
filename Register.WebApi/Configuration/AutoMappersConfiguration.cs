using AutoMapper;
using Register.Domain.Models;
using Register.WebApi.ViewModel;

namespace Register.WebApi.Configuration {
    public static class AutoMappersConfiguration {
        public static IServiceCollection AddAutoMappers(this IServiceCollection services) {
            var configuration = new MapperConfiguration(cfg => {
                cfg.AllowNullCollections = true;
                cfg.AllowNullDestinationValues = true;

                //User
                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<UserViewModel, User>();

            });
            configuration.CompileMappings();
            services.AddSingleton(configuration.CreateMapper());

            return services;
        }
    }
}
