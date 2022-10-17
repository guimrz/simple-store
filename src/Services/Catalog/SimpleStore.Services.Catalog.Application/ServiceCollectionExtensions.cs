using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SimpleStore.Services.Catalog.Application.Commands;
using SimpleStore.Services.Catalog.Application.Commands.Handlers;
using SimpleStore.Services.Catalog.Domain;
using SimpleStore.Services.Catalog.Objects.Requests;
using SimpleStore.Services.Catalog.Objects.Responses;

namespace SimpleStore.Services.Catalog.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCatalogApplication(this IServiceCollection services)
        {
            services.AddCatalogMapping();
            services.AddMediatR(typeof(CreateItemCommandHandler));

            return services;
        }

        private static IServiceCollection AddCatalogMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(configuration => 
            {
                configuration.CreateMap<CreateItemRequest, CreateItemCommand>();
                configuration.CreateMap<Item, ItemResponse>();
            });

            return services;
        }
    }
}