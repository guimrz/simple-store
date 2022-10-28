using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleStore.Services.Catalog.Application.Commands;
using SimpleStore.Services.Catalog.Application.Queries;
using SimpleStore.Services.Catalog.Application.Responses;

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
        [ProducesResponseType(typeof(CategoryResponse[]), 200)]
        public async Task<IActionResult> GetCategoriesAsync([FromQuery]GetCategoriesQuery query)
        {
            var result = await _mediator.Send(query);

            return new OkObjectResult(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CategoryResponse), 201)]
        public async Task<IActionResult> CreateCategoryAsync([FromBody]CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);

            return new ObjectResult(result);
        }
    }
}
