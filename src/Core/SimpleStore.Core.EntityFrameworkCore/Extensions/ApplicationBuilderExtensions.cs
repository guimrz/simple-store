using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleStore.Core.EntityFrameworkCore.Abstractions;

namespace SimpleStore.Core.EntityFrameworkCore.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task<IApplicationBuilder> MigrateDatabaseAsync<TContext>(this IApplicationBuilder applicationBuilder)
            where TContext : DbContext
        {
            using var scope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<TContext>();

            await context.Database.MigrateAsync();

            return applicationBuilder;
        }

        public static async Task<IApplicationBuilder> SeedDatabaseAsync(this IApplicationBuilder applicationBuilder)
        {
            using var scope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

            var logger = scope.ServiceProvider.GetRequiredService<ILogger<IApplicationBuilder>>();
            var seedingServices = scope.ServiceProvider.GetServices<ISeedingService>();

            if (seedingServices.Any())
            {
                logger.LogInformation($"Found {seedingServices.Count()} seeding services in the registered services.");

                foreach (var seedingService in seedingServices)
                {
                    logger.LogInformation($"Seeding database with data from {seedingService.GetType().FullName}.");
                    await seedingService.SeedAsync();
                }
            }
            else
            {
                logger.LogInformation("No seeding services were found in the registered services.");
            }

            return applicationBuilder;
        }
    }
}
