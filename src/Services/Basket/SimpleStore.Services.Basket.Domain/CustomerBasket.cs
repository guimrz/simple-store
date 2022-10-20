namespace SimpleStore.Services.Basket.Domain
{
    public class CustomerBasket
    {
        private List<CustomerBasketItem> _items = new List<CustomerBasketItem>();

        public Guid BuyerId { get; protected set; }

        public IEnumerable<CustomerBasketItem> Items { get => _items; }

        public DateTime CreationDate { get; }

        public DateTime? UpdateDate { get; set; }

        protected CustomerBasket()
        {
            //
        }

        public CustomerBasket(Guid buyerId)
        {
            BuyerId = buyerId == default 
                ? throw new ArgumentException("Invalid buyer id.", nameof(buyerId))
                : buyerId;

            CreationDate = DateTime.UtcNow;
        }
    }
}