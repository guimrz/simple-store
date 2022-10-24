using MediatR;
using Newtonsoft.Json;
using SimpleStore.Services.Basket.Application.Responses;

namespace SimpleStore.Services.Basket.Application.Commands
{
    public class UpdateBasketCommand : IRequest<CustomerBasketResponse>
    {
        [JsonIgnore]
        public Guid BuyerId { get; set; }

        public IEnumerable<UpdateBasketItemCommand> Items { get; set; } = new List<UpdateBasketItemCommand>();
    }
}
