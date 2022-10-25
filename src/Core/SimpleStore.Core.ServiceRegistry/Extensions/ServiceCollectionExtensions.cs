using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleStore.Core.ServiceRegistry.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceRegistry(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ServiceDiscoveryConfiguration>(options => configuration.GetSection("ServiceRegistry").Bind(options));

            return services;
        }
    }
}
