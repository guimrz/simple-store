using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SimpleStore.Services.Basket.API.Controllers
{
    [Route("basket")]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
    }
}
