using MediatR;
using SimpleStore.Services.Basket.Application.Responses;

namespace SimpleStore.Services.Basket.Application.Queries
{
    public class GetBasketQuery : IRequest<CustomerBasketResponse>
    {
        public Guid BuyerId { get; set; }
    }
}