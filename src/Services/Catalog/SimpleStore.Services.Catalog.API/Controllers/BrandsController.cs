using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleStore.Core.Mvc;
using SimpleStore.Services.Catalog.Application.Commands;
using SimpleStore.Services.Catalog.Application.Queries;
using SimpleStore.Services.Catalog.Application.Responses;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SimpleStore.Services.Catalog.API.Controllers
{
    [Route("api/brands")]
    public class BrandsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BrandsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [ProducesResponseType(typeof(BrandResponse), 201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> CreateBrandAsync([FromBody] CreateBrandCommand command)
        {
            var result = await _mediator.Send(command);

            return new ObjectResult(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(BrandResponse[]), 200)]
        public async Task<IActionResult> GetBrandsAsync([FromQuery] GetBrandsQuery query)
        {
            var result = await _mediator.Send(query);

            return new ObjectResult(result);
        }

        [HttpPut("{brandId:guid}")]
        [ProducesResponseType(typeof(BrandResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> UpdateBrandAsync(Guid brandId, [FromBody] UpdateBrandCommand command)
        {
            command.BrandId = brandId;
            var result = await _mediator.Send(command);

            return new ObjectResult(result);
        }

        [HttpDelete("{brandId:guid}")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> DeleteBrandAsync(Guid brandId)
        {
            var result = await _mediator.Send(new DeleteBrandCommand { BrandId = brandId});

            return new ObjectResult(result);
        }
    }
}
