using AutoMapper;
using Grpc.Core;
using SimpleStore.Core.EntityFrameworkCore.Abstractions;
using SimpleStore.Services.Catalog.Domain;
using SimpleStore.Services.Catalog.gRPC;

namespace SimpleStore.Services.Catalog.Grpc.Services
{
    public class ProductsService : gRPC.ProductsService.ProductsServiceBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(mapper));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override Task<ProductsDetailsReply> GetProductsDetails(ProductsDetailsRequest request, ServerCallContext context)
        {
            var products = _unitOfWork.Repository<Product>().Entities.Where(product => request.Products.Any(p => p.Equals(product.Id.ToString(), StringComparison.OrdinalIgnoreCase)));

            var reply = new ProductsDetailsReply();
            reply.Products.Add(products.Select(product => _mapper.Map<ProductDetailsReply>(product)));

            return Task.FromResult(reply);
        }
    }
}