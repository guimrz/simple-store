using MediatR;
using SimpleStore.Services.Catalog.Application.Responses;
using SimpleStore.Services.Catalog.Application.Validators;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SimpleStore.Services.Catalog.Application.Commands
{
    public class UpdateProductCommand : IRequest<ProductResponse>
    {
        [JsonIgnore]
        public Guid ProductId { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public string? PictureUrl { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        [ValidBrandValidationAttribute]
        public Guid? BrandId { get; set; }

        public IEnumerable<Guid>? Categories { get; set; }
    }
}
