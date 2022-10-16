using Microsoft.Extensions.DependencyInjection;
using SimpleStore.Services.Catalog.Application.Commands;
using SimpleStore.Services.Catalog.Objects.Requests;

namespace SimpleStore.Services.Catalog.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCatalogApplication(this IServiceCollection services)
        {
            return services.AddCatalogMapping();
        }

        private static IServiceCollection AddCatalogMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(configuration => 
            {
                configuration.CreateMap<CreateItemRequest, CreateItemCommand>();
            });

            return services;
        }
    }
}
