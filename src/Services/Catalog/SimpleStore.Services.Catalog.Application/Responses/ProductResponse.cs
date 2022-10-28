namespace SimpleStore.Services.Catalog.Application.Responses
{
    public class ProductResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public string? PictureUrl { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public ProductBrandResponse? Brand { get; set; }

        public IEnumerable<ProductCategoryResponse>? Categories { get; set; }
    }
}
