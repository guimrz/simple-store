using SimpleStore.Core.Domain;
using SimpleStore.Core.Domain.Abstractions;
using SimpleStore.Services.Ordering.Domain.Exceptions;

namespace SimpleStore.Services.Ordering.Domain
{
    public class OrderItem : EntityBase<Guid>, IEntity<Guid>
    {
        public string ProductName { get; private set; }

        public Guid ProductId { get; private set; }

        public string? ProductImageUrl { get; private set; }

        public decimal UnitPrice { get; private set; }

        public int Units { get; private set; }

        public decimal Discount { get; private set; }

        public OrderItem(Guid productId, string productName, decimal unitPrice, int units, decimal discount, string? productImageUrl)
        {
            if (units <= 0)
            {
                throw new OrderingDomainException($"The value of {nameof(Units)} must be greater than zero.");
            }

            if (discount < 0)
            {
                throw new OrderingDomainException($"The value of {nameof(Discount)} must be greater or equal to zero.");
            }

            if (unitPrice < 0)
            {
                throw new OrderingDomainException($"The value of {nameof(UnitPrice)} must be equal or greater than zero.");
            }

            if ((unitPrice * units) < discount)
            {
                throw new OrderingDomainException($"The total price is lower than the discount.");
            }

            if (productId == default)
            {
                throw new OrderingDomainException($"The value of {nameof(ProductId)} is invalid.");
            }

            if (string.IsNullOrWhiteSpace(productName))
            {
                throw new OrderingDomainException($"The value of {nameof(ProductName)} cannot be null, empty or whitespaces.");
            }

            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Units = units;
            Discount = discount;
            ProductImageUrl = productImageUrl;
        }
    }
}
