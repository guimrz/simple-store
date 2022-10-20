using SimpleStore.Services.Basket.Services.Products.Entities;

namespace SimpleStore.Services.Basket.Grpc.Services.Entities
{
    public class Product : IProduct
    {
        public Guid ProductId { get; set; }

        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public int Stock { get; set; }

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }
    }
}
