using Microsoft.Extensions.DependencyInjection;
using SimpleStore.Core.EntityFrameworkCore.Abstractions;

namespace SimpleStore.Services.Catalog.Repository.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCatalogRepository(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, CatalogUnitOfWork>();

            return services;
        }
    }
}
