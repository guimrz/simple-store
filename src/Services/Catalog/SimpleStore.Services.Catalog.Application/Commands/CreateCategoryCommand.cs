using MediatR;
using SimpleStore.Services.Catalog.Application.Responses;
using System.ComponentModel.DataAnnotations;

namespace SimpleStore.Services.Catalog.Application.Commands
{
    public class CreateCategoryCommand : IRequest<CategoryResponse>
    {
        [Required]
        public string Name { get; set; } = default!;

        public string? Description { get; set; }
    }
}
