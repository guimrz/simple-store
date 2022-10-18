using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SimpleStore.Services.Basket.Application.Queries;
using SimpleStore.Services.Basket.Application.Responses;
using SimpleStore.Services.Basket.Domain;

namespace SimpleStore.Services.Basket.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBasketApplication(this IServiceCollection services)
        {
            services.AddBasketMapping();
            services.AddMediatR(typeof(GetBasketQuery));

            return services;
        }

        private static IServiceCollection AddBasketMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(configuration =>
            {
                configuration.CreateMap<CustomerBasket, CustomerBasketResponse>();
            });

            return services;
        }
    }
}
