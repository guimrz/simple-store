using Microsoft.Extensions.DependencyInjection;
using SimpleStore.Core.EntityFrameworkCore.Abstractions;
using SimpleStore.Core.EntityFrameworkCore.Extensions;

namespace SimpleStore.Services.Catalog.Repository.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCatalogRepository(this IServiceCollection services)
        {
            services.AddDefaultUnitOfWork<CatalogDbContext>();

            return services;
        }
    }
}
