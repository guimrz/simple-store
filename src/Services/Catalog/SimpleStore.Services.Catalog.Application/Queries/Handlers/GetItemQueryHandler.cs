using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleStore.Core.EntityFrameworkCore.Abstractions;
using SimpleStore.Core.Exceptions;
using SimpleStore.Services.Catalog.Application.Responses;
using SimpleStore.Services.Catalog.Domain;

namespace SimpleStore.Services.Catalog.Application.Queries.Handlers
{
    public class GetItemQueryHandler : IRequestHandler<GetItemQuery, ItemResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetItemQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ItemResponse> Handle(GetItemQuery request, CancellationToken cancellationToken)
        {
            Item? item = await _unitOfWork.Repository<Item>().Entities.Include(item => item.Brand).SingleOrDefaultAsync(item => item.Id == request.ItemId);

            if (item is null)
            {
                throw new NotFoundException($"The itemId with id '{request.ItemId}' could not be found.");
            }

            return _mapper.Map<ItemResponse>(item);
        }
    }
}