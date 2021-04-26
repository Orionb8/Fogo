using Fogo.Configuration;
using Fogo.Data.Extensions;
using Fogo.Mappers.Extensions;
using Fogo.Selectors.Extensions;
using Fogo.Services;
using Fogo.Services.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fogo.Extensions {

    public static class IServiceCollectionExtensions {

        public static IServiceCollection AddFogoServices(this IServiceCollection services, IConfiguration configuration) {
            services.AddFogoData(configuration);
            services.AddFogoServices();
            services.AddFogoSelectors();
            services.AddFogoMappers();
            services.AddSelector();
            services.AddMapper();

            services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));
            services.Configure<TwilioSettings>(configuration.GetSection(nameof(TwilioSettings)));

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ITwilioSender, TwilioSender>();

            return services;
        }

        private static void AddFogoServices(this IServiceCollection services) {
            services.AddScoped(typeof(IReadOnlyService<,>), typeof(FogoService<,>));
        }

        private static void AddFogoMappers(this IServiceCollection services) {
        }

        private static void AddFogoSelectors(this IServiceCollection services) {
        }
    }
}