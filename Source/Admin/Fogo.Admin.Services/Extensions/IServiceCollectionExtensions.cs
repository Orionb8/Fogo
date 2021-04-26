using Fogo.Data.Extensions;
using Fogo.Mappers;
using Fogo.Mappers.Extensions;
using Fogo.Models;
using Fogo.Selectors;
using Fogo.Selectors.Extensions;
using Fogo.Services;
using Fogo.ViewModels;
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
            return services;
        }

        private static void AddFogoServices(this IServiceCollection services) {
            services.AddScoped(typeof(IService<,>), typeof(FogoService<,>));
        }

        private static void AddFogoMappers(this IServiceCollection services) {
            services.AddSingleton<IMapper<UserModel, UserViewModel>, UserMapper>();
            services.AddSingleton<IMapper<UserViewModel, UserModel>, UserMapper>();
            services.AddSingleton<IMapper<RoleModel, RoleViewModel>, RoleMapper>();
            services.AddSingleton<IMapper<RoleViewModel, RoleModel>, RoleMapper>();
            services.AddSingleton<IMapper<ExecutorModel, ExecutorViewModel>, ExecutorMapper>();
            services.AddSingleton<IMapper<ExecutorViewModel, ExecutorModel>, ExecutorMapper>();
            services.AddSingleton<IMapper<AdvertiserModel, AdvertiserViewModel>, AdvertiserMapper>();
            services.AddSingleton<IMapper<AdvertiserViewModel, AdvertiserModel>, AdvertiserMapper>();
            services.AddSingleton<IMapper<AdvertTypeViewModel, AdvertTypeModel>, AdvertTypeMapper>();
            services.AddSingleton<IMapper<AdvertTypeModel, AdvertTypeViewModel>, AdvertTypeMapper>();
            services.AddSingleton<IMapper<SocialNetworkViewModel, SocialNetworkModel>, SocialNetworkMapper>();
        }

        private static void AddFogoSelectors(this IServiceCollection services) {
            services.AddSingleton<ISelector<UserModel, UserViewModel>, UserMapper>();
            services.AddSingleton<ISelector<RoleModel, RoleViewModel>, RoleMapper>();
            services.AddSingleton<ISelector<ExecutorModel, ExecutorViewModel>, ExecutorMapper>();
            services.AddSingleton<ISelector<AdvertiserModel, AdvertiserViewModel>, AdvertiserMapper>();
            services.AddSingleton<ISelector<AdvertTypeModel, AdvertTypeViewModel>, AdvertTypeMapper>();
            services.AddSingleton<ISelector<SocialNetworkModel, SocialNetworkViewModel>, SocialNetworkMapper>();
        }
    }
}