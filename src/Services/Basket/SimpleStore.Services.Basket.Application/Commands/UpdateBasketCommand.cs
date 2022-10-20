using MediatR;
using SimpleStore.Services.Basket.Application.Responses;

namespace SimpleStore.Services.Basket.Application.Commands
{
    public class UpdateBasketCommand : IRequest<CustomerBasketResponse>
    {
        public Guid BuyerId { get; set; }

        public IEnumerable<UpdateBasketItemCommand> Items { get; set; } = default!;
    }
}
