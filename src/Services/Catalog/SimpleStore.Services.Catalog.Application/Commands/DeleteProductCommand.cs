using MediatR;

namespace SimpleStore.Services.Catalog.Application.Commands
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public Guid ProductId { get; set; }
    }
}
