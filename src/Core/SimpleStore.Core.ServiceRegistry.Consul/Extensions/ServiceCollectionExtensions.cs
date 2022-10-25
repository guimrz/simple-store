using Consul;
using Consul.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace SimpleStore.Core.ServiceRegistry.Consul.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConsulServiceRegistry(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConsulClientConfiguration>(options => configuration.GetSection("ServiceRegistry:Consul").Bind(options));
            services.TryAddSingleton<IConsulClientFactory, ConsulClientFactory>();
            services.TryAddSingleton((IServiceProvider sp) => sp.GetRequiredService<IConsulClientFactory>().CreateClient(Options.DefaultName));

            // Service registration
            services.AddHostedService<AgentServiceRegistrationHostedService>();
            services.TryAddTransient<AgentServiceRegistration>(serviceProvider =>
            {
                var serviceDiscoveryOptions = serviceProvider.GetRequiredService<IOptions<ServiceDiscoveryConfiguration>>()?.Value;

                return new AgentServiceRegistration
                {
                    Address = serviceDiscoveryOptions?.Address,
                    Name = serviceDiscoveryOptions?.ServiceName,
                    ID = Guid.NewGuid().ToString(),
                    Port = serviceDiscoveryOptions?.Port ?? 0,
                    Checks = new[]
                    {
                        new AgentCheckRegistration
                        {
                            HTTP = $"http://{serviceDiscoveryOptions?.Address}:{serviceDiscoveryOptions?.Port}/health/status",
                            Notes = "Checks /health/status",
                            Timeout = TimeSpan.FromSeconds(10),
                            Interval = TimeSpan.FromSeconds(30)
                        }
                    }
                };
            });

            return services;
        }
    }
}
