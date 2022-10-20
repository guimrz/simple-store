using Microsoft.Extensions.DependencyInjection;

namespace SimpleStore.Services.Basket.Repository
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBasketRepository(this IServiceCollection services)
        {
            services.AddScoped<IBasketRepository, CacheBasketRepository>();

            return services;
        }
    }
}
