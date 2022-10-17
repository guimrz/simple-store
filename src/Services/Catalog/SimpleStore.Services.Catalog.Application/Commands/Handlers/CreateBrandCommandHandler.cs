using AutoMapper;
using MediatR;
using SimpleStore.Core.EntityFrameworkCore.Abstractions;
using SimpleStore.Services.Catalog.Application.Responses;
using SimpleStore.Services.Catalog.Domain;

namespace SimpleStore.Services.Catalog.Application.Commands.Handlers
{
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, BrandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBrandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BrandResponse> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            Brand brand = new(request.Name, request.Description);

            brand = await _unitOfWork.Repository<Brand>().AddAsync(brand, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<BrandResponse>(brand);
        }
    }
}
