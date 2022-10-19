using MediatR;
using SimpleStore.Services.Catalog.Application.Responses;

namespace SimpleStore.Services.Catalog.Application.Queries
{
    public class GetProductQuery : IRequest<ProductResponse>
    {
        public Guid ProductId { get; set; }
    }
}
