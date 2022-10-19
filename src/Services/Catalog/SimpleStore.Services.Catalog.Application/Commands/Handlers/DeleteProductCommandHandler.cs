using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleStore.Core.EntityFrameworkCore.Abstractions;
using SimpleStore.Core.Exceptions;
using SimpleStore.Services.Catalog.Domain;

namespace SimpleStore.Services.Catalog.Application.Commands.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            IRepository<Product> repository = _unitOfWork.Repository<Product>();
            Product? product = await repository.Entities.SingleOrDefaultAsync(product => product.Id == request.ProductId);

            if (product is null)
            {
                throw new NotFoundException($"The product with id '{request.ProductId}' could not be found.");
            }

            bool result = await repository.DeleteAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return result;
        }
    }
}
