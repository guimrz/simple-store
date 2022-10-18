using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleStore.Core.EntityFrameworkCore.Abstractions;
using SimpleStore.Core.Exceptions;
using SimpleStore.Services.Catalog.Domain;

namespace SimpleStore.Services.Catalog.Application.Commands.Handlers
{
    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            IRepository<Item> repository = _unitOfWork.Repository<Item>();
            Item? item = await repository.Entities.SingleOrDefaultAsync(brand => brand.Id == request.ItemId);

            if (item is null)
            {
                throw new NotFoundException($"The item with id '{request.ItemId}' could not be found.");
            }

            bool result = await repository.DeleteAsync(item);
            await _unitOfWork.SaveChangesAsync();

            return result;
        }
    }
}
