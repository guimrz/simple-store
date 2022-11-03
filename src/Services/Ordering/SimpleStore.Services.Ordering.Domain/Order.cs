using SimpleStore.Core.Domain;
using SimpleStore.Core.Domain.Abstractions;

namespace SimpleStore.Services.Ordering.Domain
{
    public class Order : EntityBase<Guid>, IEntity<Guid>, ITimeTrackable
    {
        private readonly List<OrderItem> _items = new List<OrderItem>();
        private readonly List<OrderStatus> _statuses = new List<OrderStatus>();

        public Guid CustomerId { get; protected set; }

        public DateTime CreationDate { get; private set; }

        public DateTime? UpdateDate { get; private set; }

        public IReadOnlyCollection<OrderItem> Items { get => _items; }

        public IReadOnlyCollection<OrderStatus> Statuses { get => _statuses; }

        public Order(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}