using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleStore.Core.EntityFrameworkCore.Abstractions;
using SimpleStore.Core.Extensions;
using SimpleStore.Services.Catalog.Application.Responses;
using SimpleStore.Services.Catalog.Domain;

namespace SimpleStore.Services.Catalog.Application.Queries.Handlers
{
    public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, IEnumerable<ItemResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetItemsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ItemResponse>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Item> items = _unitOfWork.Repository<Item>().Entities.Include(p => p.Brand).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                items = items.Where(item => item.Name.Contains(request.Search.Trim()));
            }

            items = items.Paginate(request.Page, request.PageSize);

            return _mapper.Map<IEnumerable<ItemResponse>>(await items.ToListAsync());
        }
    }
}
