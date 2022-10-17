using AutoMapper;
using MediatR;
using SimpleStore.Core.EntityFrameworkCore.Abstractions;
using SimpleStore.Services.Catalog.Domain;
using SimpleStore.Services.Catalog.Objects.Responses;

namespace SimpleStore.Services.Catalog.Application.Commands.Handlers
{
    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, ItemResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ItemResponse> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            Item item = new Item(request.Name, request.Description);

            item = await _unitOfWork.Repository<Item>().AddAsync(item, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ItemResponse>(item);
        }
    }
}
