using MediatR;
using SimpleStore.Services.Catalog.Application.Responses;
using SimpleStore.Services.Catalog.Application.Validators;
using System.ComponentModel.DataAnnotations;

namespace SimpleStore.Services.Catalog.Application.Commands
{
    public class CreateProductCommand : IRequest<ProductResponse>
    {
        [Required]
        [MaxLength(128)]
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        [Required]
        [ValidBrandValidationAttribute]
        public Guid BrandId { get; set; }
    }
}
