namespace SimpleStore.Services.Basket.Application.Responses
{
    public class CustomerBasketItemResponse
    {
        public Guid ProductId { get; set; }

        public string Name { get; set; } = default!;

        public string? Description { get; set; } = default!;

        public int Quantity { get; set; } = default;

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }
    }
}
