using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleStore.Core.EntityFrameworkCore.Abstractions;
using SimpleStore.Core.Exceptions;
using SimpleStore.Services.Catalog.Application.Responses;
using SimpleStore.Services.Catalog.Domain;

namespace SimpleStore.Services.Catalog.Application.Commands.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<ProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Product? product = await _unitOfWork.Repository<Product>().Entities
                .Include(product => product.Brand)
                .Include(product => product.Categories)
                .SingleOrDefaultAsync(product => product.Id == request.ProductId);

            if (product is null)
            {
                throw new NotFoundException($"The product with id '{request.ProductId}' could not be found.");
            }


            Brand? brand = await _unitOfWork.Repository<Brand>().Entities.SingleOrDefaultAsync(brand => brand.Id == request.BrandId);

            if (brand is null)
            {
                throw new ArgumentException(nameof(request.BrandId), $"The brand with id '{request.BrandId}' could not be found.");
            }

            // Update the categories
            if (product.Categories.Any())
            {
                product.RemoveAll();

                if (request.Categories != null || request.Categories!.Any())
                {
                    var categoriesRepository = _unitOfWork.Repository<Category>();
                    foreach (var categoryId in request.Categories!.Distinct())
                    {
                        var category = await categoriesRepository.Entities.SingleOrDefaultAsync(p => p.Id == categoryId);

                        if (category is null)
                        {
                            throw new ArgumentException($"The category with id '{categoryId}' could not be found.");
                        }

                        product.Add(category);
                    }
                }
            }

            product.Name = request.Name;
            product.Description = request.Description;
            product.Brand = brand;
            product.PictureUrl = request.PictureUrl;

            product.UpdatePrice(request.Price);
            product.UpdateStock(request.Stock);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ProductResponse>(product);
        }
    }
}