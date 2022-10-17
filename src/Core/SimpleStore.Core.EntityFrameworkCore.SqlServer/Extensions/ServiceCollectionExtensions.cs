using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleStore.Core.EntityFrameworkCore.SqlServer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSqlDatabase<TContext>(this IServiceCollection services, IConfiguration configuration, string connectionName = "DefaultConnection")
            where TContext : DbContext
        {
            string connectionString = configuration.GetConnectionString(connectionName);

            services.AddDbContext<TContext>(options =>
            {
                options.UseSqlServer(connectionString, o =>
                {
                    o.EnableRetryOnFailure();
                });
            });

            return services;
        }
    }
}
