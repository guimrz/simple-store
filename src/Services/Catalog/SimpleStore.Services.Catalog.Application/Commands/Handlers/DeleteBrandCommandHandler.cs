using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleStore.Core.EntityFrameworkCore.Abstractions;
using SimpleStore.Core.Exceptions;
using SimpleStore.Services.Catalog.Domain;

namespace SimpleStore.Services.Catalog.Application.Commands.Handlers
{
    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBrandCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<bool> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            IRepository<Brand> repository = _unitOfWork.Repository<Brand>();
            Brand? brand = await repository.Entities.SingleOrDefaultAsync(brand => brand.Id == request.BrandId);

            if (brand is null)
            {
                throw new NotFoundException($"The brand with id '{request.BrandId}' could not be found.");
            }

            bool result = await repository.DeleteAsync(brand);
            await _unitOfWork.SaveChangesAsync();

            return result;
        }
    }
}
