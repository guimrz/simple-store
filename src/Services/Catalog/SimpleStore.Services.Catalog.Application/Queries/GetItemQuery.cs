using MediatR;
using SimpleStore.Services.Catalog.Application.Responses;

namespace SimpleStore.Services.Catalog.Application.Queries
{
    public class GetItemQuery : IRequest<ItemResponse>
    {
        public Guid ItemId { get; set; }
    }
}
