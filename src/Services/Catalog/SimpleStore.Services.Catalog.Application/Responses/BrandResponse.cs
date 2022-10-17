namespace SimpleStore.Services.Catalog.Application.Responses
{
    public class BrandResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
