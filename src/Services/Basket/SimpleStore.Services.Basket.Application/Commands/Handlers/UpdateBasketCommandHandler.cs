using MediatR;
using SimpleStore.Services.Basket.Application.Queries;
using SimpleStore.Services.Basket.Application.Responses;
using SimpleStore.Services.Basket.Domain;
using SimpleStore.Services.Basket.Repository;

namespace SimpleStore.Services.Basket.Application.Commands.Handlers
{
    public class UpdateBasketCommandHandler : IRequestHandler<UpdateBasketCommand, CustomerBasketResponse>
    {
        private readonly IMediator _mediator;
        private readonly IBasketRepository _basketRepository;

        public UpdateBasketCommandHandler(IMediator mediator, IBasketRepository basketRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
        }

        public async Task<CustomerBasketResponse> Handle(UpdateBasketCommand request, CancellationToken cancellationToken)
        {
            CustomerBasket basket = new CustomerBasket(request.BuyerId);

            foreach(var item in request.Items)
            {
                (basket.Items as List<CustomerBasketItem>)?.Add(new CustomerBasketItem(item.ProductId, item.Quantity));
            }

            await _basketRepository.UpdateAsync(basket);

            return await _mediator.Send(new GetBasketQuery { BuyerId = request.BuyerId }, cancellationToken);
        }
    }
}
