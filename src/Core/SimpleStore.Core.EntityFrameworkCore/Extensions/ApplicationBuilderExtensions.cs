using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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

        public static IApplicationBuilder MigrateDatabase<TContext>(this IApplicationBuilder applicationBuilder)
           where TContext : DbContext
        {
            using var scope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<TContext>();

            context.Database.Migrate();

            return applicationBuilder;
        }
    }
}
