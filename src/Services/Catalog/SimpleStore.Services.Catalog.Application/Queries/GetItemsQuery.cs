using MediatR;
using SimpleStore.Services.Catalog.Application.Responses;
using System.ComponentModel.DataAnnotations;

namespace SimpleStore.Services.Catalog.Application.Queries
{
    public class GetItemsQuery : IRequest<IEnumerable<ItemResponse>>
    {
        public string? Search { get; set; }

        [Range(1, int.MaxValue)]
        public int Page { get; set; } = 1;

        [Range(1, int.MaxValue)]
        public int PageSize { get; set; } = 20;
    }
}
