using Microsoft.Extensions.DependencyInjection;
using SimpleStore.Services.Catalog.Domain;
using SimpleStore.Services.Catalog.gRPC;

namespace SimpleStore.Services.Catalog.Grpc
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCatalogGrpcServer(this IServiceCollection services)
        {
            _ = services.AddGrpc();
            services.AddCatalogGrpcMapping();

            return services;
        }

        private static IServiceCollection AddCatalogGrpcMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(configuration =>
            {
                configuration.CreateMap<Product, ProductDetailsReply>()
                    .ForMember(target => target.ProductId, map => map.MapFrom(source => source.Id.ToString()))
                    .ForMember(target => target.Name, map => map.MapFrom(source => source.Name))
                    .ForMember(target => target.Description, map => map.MapFrom(source => source.Description))
                    .ForMember(target => target.Price, map => map.MapFrom(source => source.Price))
                    .ForMember(target => target.Stock, map => map.MapFrom(source => source.Stock));
            });

            return services;
        }
    }
}
