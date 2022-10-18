using MediatR;

namespace SimpleStore.Services.Catalog.Application.Commands
{
    public class DeleteBrandCommand : IRequest<bool>
    {
        public Guid BrandId { get; set; }
    }
}
