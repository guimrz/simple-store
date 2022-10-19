using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleStore.Core.Mvc;
using SimpleStore.Services.Catalog.Application.Commands;
using SimpleStore.Services.Catalog.Application.Queries;
using SimpleStore.Services.Catalog.Application.Responses;

namespace SimpleStore.Services.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductResponse), 201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);

            return new ObjectResult(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ProductResponse[]), 200)]
        public async Task<IActionResult> GetProductsAsync([FromQuery] GetProductsQuery query)
        {
            var result = await _mediator.Send(query);

            return new ObjectResult(result);
        }

        [HttpGet("{productId:guid}")]
        [ProducesResponseType(typeof(ProductResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> GetProductAsync(Guid productId)
        {
            var result = await _mediator.Send(new GetProductQuery { ProductId = productId });

            return new ObjectResult(result);
        }

        [HttpPut("{productId:guid}")]
        [ProducesResponseType(typeof(ProductResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> UpdateProductAsync(Guid productId, [FromBody] UpdateProductCommand updateItemCommand)
        {
            updateItemCommand.ProductId = productId;
            var result = await _mediator.Send(updateItemCommand);

            return new ObjectResult(result);
        }

        [HttpDelete("{productId:guid}")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> DeleteProductAsync(Guid productId)
        {
            var result = await _mediator.Send(new DeleteProductCommand { ProductId = productId });

            return new ObjectResult(result);
        }
    }
}