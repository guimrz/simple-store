using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleStore.Core.EntityFrameworkCore.Abstractions;
using SimpleStore.Core.Exceptions;
using SimpleStore.Services.Catalog.Application.Responses;
using SimpleStore.Services.Catalog.Domain;
using System.ComponentModel.DataAnnotations;

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
            Brand? brand = await _unitOfWork.Repository<Brand>().All.SingleOrDefaultAsync(brand => brand.Id == request.BrandId);

            if(brand is null)
            {
                throw new ArgumentException(nameof(request.BrandId), $"The brand with id '{request.BrandId}' could not be found.");
            }

            Item item = new Item(request.Name, request.Description, brand);

            item = await _unitOfWork.Repository<Item>().AddAsync(item, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ItemResponse>(item);
        }
    }
}
