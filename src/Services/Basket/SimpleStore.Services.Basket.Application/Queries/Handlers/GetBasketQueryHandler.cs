using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleStore.Core.EntityFrameworkCore.Abstractions;
using SimpleStore.Services.Basket.Application.Responses;
using SimpleStore.Services.Basket.Application.Services.Products;
using SimpleStore.Services.Basket.Domain;
using SimpleStore.Services.Basket.Repository;

namespace SimpleStore.Services.Basket.Application.Queries.Handlers
{
    public class GetBasketQueryHandler : IRequestHandler<GetBasketQuery, CustomerBasketResponse>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IProductsService _productsService;
        public GetBasketQueryHandler(IBasketRepository basketRepository, IProductsService productsService)
        {
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
            _productsService = productsService ?? throw new ArgumentNullException(nameof(productsService));
        }

        public async Task<CustomerBasketResponse> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.GetAsync(request.BuyerId);

            List<CustomerBasketItemResponse> basketItems = new();
            CustomerBasketResponse response = new CustomerBasketResponse { Products = basketItems };
            if (basket?.Items != null && basket!.Items.Any())
            {
                var products = await _productsService.GetProductsInformationAsync(basket.Items.Select(product => product.ProductId));

                foreach(var product in products)
                {
                    var basketItem = basket.Items.First(p => p.ProductId == product.ProductId);

                    CustomerBasketItemResponse item = new()
                    {
                        Description = product.Description,
                        Name = product.Name,
                        Quantity = basketItem.Quantity,
                        ImageUrl = product.ImageUrl,
                        Price = product.Price,
                        ProductId = product.ProductId
                    };

                    basketItems.Add(item);
                }
            }

            return response;
        }
    }
}