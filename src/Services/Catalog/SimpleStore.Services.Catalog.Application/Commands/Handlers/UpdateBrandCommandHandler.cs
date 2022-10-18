using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleStore.Core.EntityFrameworkCore.Abstractions;
using SimpleStore.Core.Exceptions;
using SimpleStore.Services.Catalog.Application.Responses;
using SimpleStore.Services.Catalog.Domain;

namespace SimpleStore.Services.Catalog.Application.Commands.Handlers
{
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, BrandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBrandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<BrandResponse> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            Brand? brand = await _unitOfWork.Repository<Brand>().Entities.SingleOrDefaultAsync(brand => brand.Id == request.BrandId);

            if (brand is null)
            {
                throw new NotFoundException($"The brand with id '{request.BrandId}' could not be found.");
            }

            brand.Name = request.Name;
            brand.Description = request.Description;

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<BrandResponse>(brand);
        }
    }
}
