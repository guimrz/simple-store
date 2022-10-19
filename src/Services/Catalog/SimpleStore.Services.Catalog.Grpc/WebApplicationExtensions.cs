using Microsoft.AspNetCore.Builder;
using SimpleStore.Services.Catalog.Grpc.Services;

namespace SimpleStore.Services.Catalog.Grpc
{
    public static class WebApplicationExtensions
    {
        public static WebApplication UseCatalogGrpc(this WebApplication application)
        {
            application.MapGrpcService<ProductsService>();

            return application;
        }
    }
}
