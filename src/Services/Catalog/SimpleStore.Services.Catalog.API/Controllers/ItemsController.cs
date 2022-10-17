using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleStore.Services.Catalog.Application.Commands;
using SimpleStore.Services.Catalog.Objects.Queries;
using SimpleStore.Services.Catalog.Objects.Requests;
using SimpleStore.Services.Catalog.Objects.Responses;

namespace SimpleStore.Services.Catalog.API.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ItemsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ItemResponse), 201)]
        public async Task<IActionResult> CreateItemAsync([FromBody] CreateItemRequest request)
        {
            var command = _mapper.Map<CreateItemCommand>(request);

            var result = await _mediator.Send(command);

            return new ObjectResult(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ItemResponse[]), 200)]
        public Task<IActionResult> GetItemsAsync([FromQuery]GetItemsQuery query)
        {
            throw new NotImplementedException();
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