using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleStore.Core.EntityFrameworkCore.Abstractions;
using SimpleStore.Services.Catalog.Application.Responses;
using SimpleStore.Services.Catalog.Domain;

namespace SimpleStore.Services.Catalog.Application.Queries.Handlers
{
    public class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, IEnumerable<BrandResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBrandsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<BrandResponse>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Brand> brands = _unitOfWork.Repository<Brand>().All.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                brands = brands.Where(brand => brand.Name.Contains(request.Search.Trim()));
            }

            brands = brands.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize);

            return _mapper.Map<IEnumerable<BrandResponse>>(await brands.ToListAsync());
        }
    }
}
