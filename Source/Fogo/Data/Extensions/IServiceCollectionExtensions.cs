using Fogo.Data.Repositories;
using Fogo.Data.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Fogo.Data.Extensions {

    public static class IServiceCollectionExtensions {

        public static IServiceCollection AddRepository<TDbContext, TModel>(
            this IServiceCollection services)
            where TDbContext : DbContext
            where TModel : class {
            return services.AddScoped<IRepository<TModel>, Repository<TDbContext, TModel>>();
        }
    }
}