using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleStore.Services.Basket.Application.Services.Products;
using SimpleStore.Services.Catalog.gRPC;
using SimpleStore.Services.Basket.Grpc.Services;
using SimpleStore.Services.Basket.Grpc.Services.Entities;
using static SimpleStore.Services.Catalog.gRPC.ProductsProtoService;
using SimpleStore.Services.Basket.Services.Products.Entities;

namespace SimpleStore.Services.Basket.Grpc
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBasketGrpc(this IServiceCollection services)
        {
            services.AddGrpcClient<ProductsProtoServiceClient>((serviceProvider, options) =>
            {
                options.Address = new Uri(serviceProvider.GetRequiredService<IConfiguration>().GetSection("GrpcClients:Catalog:Address").Value);

                if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                {
                    options.ChannelOptionsActions.Add((o) =>
                    {
                        o.HttpHandler = new HttpClientHandler
                        {
                            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                        };
                    });
                }                
            });
            services.AddScoped<IProductsService, ProductsService>();

            services.AddAutoMapper(configuration =>
            {
                configuration.CreateMap<ProductDetailsReply, IProduct>()
                    .As<Product>();
                configuration.CreateMap<ProductDetailsReply, Product>()
                    .ForMember(target => target.ProductId, map => map.MapFrom(source => new Guid(source.ProductId)))
                    .ForMember(target => target.Name, map => map.MapFrom(source => source.Name))
                    .ForMember(target => target.Description, map => map.MapFrom(source => source.Description))
                    .ForMember(target => target.Price, map => map.MapFrom(source => source.Price))
                    .ForMember(target => target.ImageUrl, map => map.MapFrom(source => source.ImageUrl));
            });

            return services;
        }
    }
}
