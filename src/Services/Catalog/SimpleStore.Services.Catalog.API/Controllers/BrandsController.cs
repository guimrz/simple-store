using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleStore.Services.Catalog.Application.Commands;
using SimpleStore.Services.Catalog.Application.Queries;
using SimpleStore.Services.Catalog.Application.Responses;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SimpleStore.Services.Catalog.API.Controllers
{
    [Route("brands")]
    public class BrandsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BrandsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [ProducesResponseType(typeof(BrandResponse), 201)]
        public async Task<IActionResult> CreateBrandAsync([FromBody] CreateBrandCommand command)
        {
            var result = await _mediator.Send(command);

            return new ObjectResult(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(BrandResponse[]), 200)]
        public async Task<IActionResult> GetBrandsAsync([FromQuery]GetBrandsQuery query)
        {
            var result = await _mediator.Send(query);

            return new ObjectResult(result);
        }

        [HttpPut("{brandId:guid}")]
        public Task<IActionResult> UpdateBrandAsync(Guid brandId, object request)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{brandId:guid}")]
        public Task<IActionResult> DeleteBrandAsync(Guid brandId)
        {
            throw new NotImplementedException();
        }
    }
}
