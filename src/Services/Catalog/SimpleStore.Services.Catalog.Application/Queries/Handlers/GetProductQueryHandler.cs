using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleStore.Core.EntityFrameworkCore.Abstractions;
using SimpleStore.Core.Exceptions;
using SimpleStore.Services.Catalog.Application.Responses;
using SimpleStore.Services.Catalog.Domain;

namespace SimpleStore.Services.Catalog.Application.Queries.Handlers
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ProductResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            Product? product = await _unitOfWork.Repository<Product>()
                .Entities.Include(product => product.Brand)
                .Include(p => p.Categories)
                .SingleOrDefaultAsync(product => product.Id == request.ProductId);

            if (product is null)
            {
                throw new NotFoundException($"The product with id '{request.ProductId}' could not be found.");
            }

            return _mapper.Map<ProductResponse>(product);
        }
    }
}