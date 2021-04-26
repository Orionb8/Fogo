using Fogo.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fogo.Data.Extensions {

    public static class IServiceCollectionExtensions {

        public static IServiceCollection AddFogoData(
            this IServiceCollection services,
            IConfiguration configuration,
            string connectionString = "DefaultConnection") {
            services.AddDbContext<FogoDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString(connectionString));
            });
            services.AddScoped(typeof(IRepository<>), typeof(FogoRepository<>));
            return services;
        }
    }
}