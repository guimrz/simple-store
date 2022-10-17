using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> CreateItemAsync([FromBody] CreateItemCommand command)
        {
            var result = await _mediator.Send(command);

            return new ObjectResult(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ItemResponse[]), 200)]
        public async Task<IActionResult> GetItemsAsync([FromQuery]GetItemsQuery query)
        {
            var result = await _mediator.Send(query);

            return new ObjectResult(result);
        }

        [HttpGet("{itemId:guid}")]
        [ProducesResponseType(typeof(ItemResponse), 200)]
        public Task<IActionResult> GetItemAsync(Guid itemId)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{itemId:guid}")]
        [ProducesResponseType(typeof(ItemResponse), 200)]
        public Task<IActionResult> UpdateItemAsync(Guid itemId)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{itemId:guid}")]
        public Task<IActionResult> DeleteItemAsync(Guid itemId)
        {
            throw new NotImplementedException();
        }
    }
}