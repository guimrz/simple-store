using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleStore.Core.EntityFrameworkCore.Abstractions;
using SimpleStore.Core.Extensions;
using SimpleStore.Services.Catalog.Application.Responses;
using SimpleStore.Services.Catalog.Domain;

namespace SimpleStore.Services.Catalog.Application.Queries.Handlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ProductResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Product> products = _unitOfWork.Repository<Product>().Entities.Include(p => p.Brand).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                products = products.Where(product => product.Name.Contains(request.Search.Trim()));
            }

            products = products.Paginate(request.Page, request.PageSize);

            return _mapper.Map<IEnumerable<ProductResponse>>(await products.ToListAsync());
        }
    }
}
