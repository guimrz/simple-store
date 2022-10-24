using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleStore.Services.Basket.Application.Commands;
using SimpleStore.Services.Basket.Application.Queries;

namespace SimpleStore.Services.Basket.API.Controllers
{
    [ApiController]
    [Route("api/basket")]
    [Authorize("UserOnly")]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetBasketAsync()
        {
            Guid buyerId = new Guid(HttpContext.User.Claims.Single(p => p.Type == "sub").Value);

            var result = await _mediator.Send(new GetBasketQuery { BuyerId = buyerId });

            return new ObjectResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBasketAsync([FromBody] UpdateBasketCommand command)
        {
            command.BuyerId = new Guid(HttpContext.User.Claims.Single(p => p.Type == "sub").Value);

            var result = await _mediator.Send(command);

            return new ObjectResult(result);
        }
    }
}