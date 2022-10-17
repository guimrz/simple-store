using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public Task<IActionResult> CreateBrandAsync([FromBody] object request)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public Task<IActionResult> GetBrandsAsync([FromQuery]object query)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{brandId:guid}")]
        public Task<IActionResult> GetBrandAsync(Guid brandId)
        {
            throw new NotImplementedException();
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
