namespace SimpleStore.Services.Basket.Services.Products.Entities
{
    public interface IProduct
    {
        Guid ProductId { get; set; }

        string Name { get; set; } 

        string? Description { get; set; }

        int Stock { get; set; }

        decimal Price { get; set; }

        string? ImageUrl { get; set; }
    }
}