using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleStore.Services.Catalog.Application.Queries;

namespace SimpleStore.Services.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    [Authorize("UserOnly")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetCategoriesQuery[]), 200)]
        public async Task<IActionResult> GetCategoriesAsync([FromQuery]GetCategoriesQuery query)
        {
            var result = await _mediator.Send(query);

            return new OkObjectResult(result);
        }
    }
}
