namespace SimpleStore.Services.Basket.Application.Commands
{
    public class UpdateBasketItemCommand
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
