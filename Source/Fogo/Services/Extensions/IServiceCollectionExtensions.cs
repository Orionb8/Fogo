using Fogo.Data.Repositories;
using Fogo.Models;
using Fogo.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Fogo.Services.Extensions {

    public static class IServiceCollectionExtensions {

        public static IServiceCollection AddService<TModel>(this IServiceCollection services)
            where TModel : class, IComparableModel<TModel>, new() {
            return services.AddScoped<IService<TModel>, BaseService<IRepository<TModel>, TModel>>();
        }

        public static IServiceCollection AddReadOnlyService<TModel>(this IServiceCollection services)
            where TModel : class, IComparableModel<TModel>, new() {
            return services.AddScoped<IReadOnlyService<TModel>, ReadOnlyService<IRepository<TModel>, TModel>>();
        }

        public static IServiceCollection AddService<TModel, TViewModel>(this IServiceCollection services)
            where TModel : class, new()
            where TViewModel : class, IComparableModel<TModel>, new() {
            return services.AddScoped<IService<TModel, TViewModel>, BaseService<IRepository<TModel>, TModel, TViewModel>>();
        }

        public static IServiceCollection AddReadOnlyService<TModel, TViewModel>(this IServiceCollection services)
            where TModel : class, new()
            where TViewModel : class, IComparableModel<TModel>, new() {
            return services.AddScoped<IReadOnlyService<TModel, TViewModel>, ReadOnlyService<IRepository<TModel>, TModel, TViewModel>>();
        }
    }
}