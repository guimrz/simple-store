using MediatR;

namespace SimpleStore.Services.Catalog.Domain.Events
{
    public class PriceUpdatedDomainEvent : INotification
    {
        public Guid ProductId { get; set; }

        public decimal NewPrice { get; set; }

        public decimal OldPrice { get; set; }

        public PriceUpdatedDomainEvent(Guid productId, decimal newPrice, decimal oldPrice)
        {
            ProductId = productId;
            NewPrice = newPrice;
            OldPrice = oldPrice;
        }
    }
}
