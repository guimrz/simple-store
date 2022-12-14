using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleStore.Core.EntityFrameworkCore.Abstractions;
using SimpleStore.Services.Catalog.Application.Responses;
using SimpleStore.Services.Catalog.Domain;

namespace SimpleStore.Services.Catalog.Application.Commands.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = new Product(request.Name, request.Price, request.Stock, request.Description);

            if (request.BrandId != null)
            {
                Brand? brand = await _unitOfWork.Repository<Brand>().Entities.SingleOrDefaultAsync(brand => brand.Id == request.BrandId);

                if (brand is null)
                {
                    throw new ArgumentException(nameof(request.BrandId), $"The brand with id '{request.BrandId}' could not be found.");
                }

                product.Brand = brand;
            }

            if (request.Categories != null && request.Categories.Any())
            {
                var categoriesRepository = _unitOfWork.Repository<Category>();

                foreach (var categoryId in request.Categories.Distinct())
                {
                    var category = await categoriesRepository.Entities.SingleOrDefaultAsync(c => c.Id == categoryId);

                    if (category is null)
                    {
                        throw new ArgumentException($"The category with id '{categoryId}' could not be found.");
                    }

                    product.Add(category);
                }
            }

            product = await _unitOfWork.Repository<Product>().AddAsync(product, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ProductResponse>(product);
        }
    }
}
