using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleStore.Core.Mvc;
using SimpleStore.Services.Catalog.Application.Commands;
using SimpleStore.Services.Catalog.Application.Queries;
using SimpleStore.Services.Catalog.Application.Responses;

namespace SimpleStore.Services.Catalog.API.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ItemsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ItemResponse), 201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> CreateItemAsync([FromBody] CreateItemCommand command)
        {
            var result = await _mediator.Send(command);

            return new ObjectResult(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ItemResponse[]), 200)]
        public async Task<IActionResult> GetItemsAsync([FromQuery] GetItemsQuery query)
        {
            var result = await _mediator.Send(query);

            return new ObjectResult(result);
        }

        [HttpGet("{itemId:guid}")]
        [ProducesResponseType(typeof(ItemResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> GetItemAsync(Guid itemId)
        {
            var result = await _mediator.Send(new GetItemQuery { ItemId = itemId });

            return new ObjectResult(result);
        }

        [HttpPut("{itemId:guid}")]
        [ProducesResponseType(typeof(ItemResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> UpdateItemAsync(Guid itemId, [FromBody] UpdateItemCommand updateItemCommand)
        {
            updateItemCommand.ItemId = itemId;
            var result = await _mediator.Send(updateItemCommand);

            return new ObjectResult(result);
        }

        [HttpDelete("{itemId:guid}")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> DeleteItemAsync(Guid itemId)
        {
            var result = await _mediator.Send(new DeleteItemCommand { ItemId = itemId });

            return new ObjectResult(result);
        }
    }
}