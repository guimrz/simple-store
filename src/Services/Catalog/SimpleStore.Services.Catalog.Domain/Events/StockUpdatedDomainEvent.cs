using MediatR;

namespace SimpleStore.Services.Catalog.Domain.Events
{
    public class StockUpdatedDomainEvent : INotification
    {
        public Guid ProductId { get; set; }

        public int Stock { get; set; }

        public StockUpdatedDomainEvent(Guid productId, int stock)
        {
            ProductId = productId;
            Stock = stock;
        }
    }
}
