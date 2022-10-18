using MediatR;

namespace SimpleStore.Services.Catalog.Application.Commands
{
    public class DeleteItemCommand : IRequest<bool>
    {
        public Guid ItemId { get; set; }
    }
}
