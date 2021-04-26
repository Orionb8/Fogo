using Fogo.Selectors.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Fogo.Selectors.Extensions {

    public static class IServiceCollectionExtensions {

        public static IServiceCollection AddSelector(this IServiceCollection services) {
            services.AddSingleton<ISelector, Selector>();
            return services;
        }
    }
}