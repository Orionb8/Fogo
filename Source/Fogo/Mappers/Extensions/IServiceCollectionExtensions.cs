using Fogo.Mappers.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Fogo.Mappers.Extensions {

    public static class IServiceCollectionExtensions {

        public static IServiceCollection AddMapper(this IServiceCollection services) {
            services.AddSingleton<IMapper, Mapper>();
            return services;
        }
    }
}