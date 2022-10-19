using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SimpleStore.Services.Catalog.Application.Commands.Handlers;
using SimpleStore.Services.Catalog.Application.Responses;
using SimpleStore.Services.Catalog.Domain;

namespace SimpleStore.Services.Catalog.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCatalogApplication(this IServiceCollection services)
        {
            services.AddCatalogMapping();
            services.AddMediatR(typeof(CreateProductCommandHandler));

            return services;
        }

        private static IServiceCollection AddCatalogMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(configuration => 
            {
                configuration.CreateMap<Product, ProductResponse>();
                configuration.CreateMap<Brand, ProductBrandResponse>();
                configuration.CreateMap<Brand, BrandResponse>();
            });


            return services;
        }
    }
}