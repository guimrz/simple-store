using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleStore.Services.Catalog.Application.Commands;
using SimpleStore.Services.Catalog.Objects.Requests;

namespace SimpleStore.Services.Catalog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public async Task<IActionResult> CreateItemAsync([FromBody] CreateItemRequest request)
        {
            var command = _mapper.Map<CreateItemCommand>(request);

            var result = await _mediator.Send(command);

            return new ObjectResult(result);
        }
    }
}