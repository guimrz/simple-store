using AutoMapper;
using SimpleStore.Services.Basket.Application.Services.Products;
using SimpleStore.Services.Basket.Grpc.Services.Entities;
using SimpleStore.Services.Basket.Services.Products.Entities;
using SimpleStore.Services.Catalog.gRPC;
using static SimpleStore.Services.Catalog.gRPC.ProductsProtoService;

namespace SimpleStore.Services.Basket.Grpc.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ProductsProtoServiceClient _client;
        private readonly IMapper _mapper;

        public ProductsService(ProductsProtoServiceClient client, IMapper mapper)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<IProduct>> GetProductsInformationAsync(IEnumerable<Guid> products)
        {
            var request = new ProductsDetailsRequest();
            request.Products.AddRange(products.Select(productId => productId.ToString()));

            var reply = await _client.GetProductsDetailsAsync(request);

            return _mapper.Map<IEnumerable<Product>>(reply.Products.ToList());
        }
    }
}
