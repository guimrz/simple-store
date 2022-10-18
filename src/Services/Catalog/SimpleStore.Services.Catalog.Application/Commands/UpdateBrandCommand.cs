using MediatR;
using SimpleStore.Services.Catalog.Application.Responses;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SimpleStore.Services.Catalog.Application.Commands
{
    public class UpdateBrandCommand : IRequest<BrandResponse>
    {
        [JsonIgnore]
        public Guid BrandId { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; } = default!;

        public string? Description { get; set; }
    }
}
