namespace SimpleStore.Services.Basket.Domain
{
    public class CustomerBasketItem
    {
        public Guid ProductId { get; protected set; }

        public int Quantity { get; set; }

        protected CustomerBasketItem()
        {
            //
        }

        public CustomerBasketItem(Guid productId, int quantity)
        {
            ProductId = productId == default
                ? throw new ArgumentException("Invalid product id.", nameof(productId))
                : productId;

            Quantity = quantity >= 0
                ? throw new ArgumentException("The quantity must be greater than zero.", nameof(quantity))
                : quantity;
        }
    }
}