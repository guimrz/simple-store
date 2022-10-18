using MediatR;
using SimpleStore.Services.Catalog.Application.Responses;
using SimpleStore.Services.Catalog.Application.Validators;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SimpleStore.Services.Catalog.Application.Commands
{
    public class UpdateItemCommand : IRequest<ItemResponse>
    {
        [JsonIgnore]
        public Guid ItemId { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        [Required]
        [ValidBrandValidationAttribute]
        public Guid BrandId { get; set; }
    }
}
