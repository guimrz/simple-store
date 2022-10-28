using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleStore.Core.EntityFrameworkCore.Abstractions;
using SimpleStore.Core.EntityFrameworkCore.Interceptors;

namespace SimpleStore.Core.EntityFrameworkCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDefaultUnitOfWork<TContext>(this IServiceCollection services)
            where TContext : DbContext
        {
            services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();
            services.AddChangeTrackerInterceptor<TimeTrackableInterceptor>();

            return services;
        }

        public static IServiceCollection AddChangeTrackerInterceptor<TInterceptor>(this IServiceCollection services)
            where TInterceptor : class, IChangeTrackerInterceptor
        {
            services.AddTransient<IChangeTrackerInterceptor, TInterceptor>();

            return services;
        }
    }
}
