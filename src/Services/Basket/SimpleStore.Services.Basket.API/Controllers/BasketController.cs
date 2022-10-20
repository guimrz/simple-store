using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleStore.Services.Basket.Application.Commands;
using SimpleStore.Services.Basket.Application.Queries;

namespace SimpleStore.Services.Basket.API.Controllers
{
    [Route("api/basket")]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{buyerId:guid}")]
        public async Task<IActionResult> GetBasketAsync(Guid buyerId)
        {
            // TODO: Get the buyer id from session.

            var result = await _mediator.Send(new GetBasketQuery { BuyerId = buyerId });

            return new ObjectResult(result);
        }

        [HttpPost("{buyerId:guid}")]
        public async Task<IActionResult> UpdateBasketAsync(Guid buyerId, [FromBody]UpdateBasketCommand command)
        {
            // TODO: Get the buyer id from session.

            command.BuyerId = buyerId;

            var result = await _mediator.Send(command);

            return new ObjectResult(result);
        }
    }
}
