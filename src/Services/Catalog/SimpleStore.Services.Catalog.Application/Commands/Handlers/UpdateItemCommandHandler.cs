using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleStore.Core.EntityFrameworkCore.Abstractions;
using SimpleStore.Core.Exceptions;
using SimpleStore.Services.Catalog.Application.Responses;
using SimpleStore.Services.Catalog.Domain;

namespace SimpleStore.Services.Catalog.Application.Commands.Handlers
{
    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, ItemResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<ItemResponse> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            Item? item = await _unitOfWork.Repository<Item>().Entities.Include(item => item.Brand).SingleOrDefaultAsync(item => item.Id == request.ItemId);

            if (item is null)
            {
                throw new NotFoundException($"The itemId with id '{request.ItemId}' could not be found.");
            }


            Brand? brand = await _unitOfWork.Repository<Brand>().Entities.SingleOrDefaultAsync(brand => brand.Id == request.BrandId);

            if (brand is null)
            {
                throw new ArgumentException(nameof(request.BrandId), $"The brand with id '{request.BrandId}' could not be found.");
            }

            item.Name = request.Name;
            item.Description = request.Description;
            item.Brand = brand;

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ItemResponse>(item);
        }
    }
}