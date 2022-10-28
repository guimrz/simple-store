using SimpleStore.Core.Domain;
using SimpleStore.Core.Domain.Abstractions;

namespace SimpleStore.Services.Ordering.Domain
{
    public class Order : EntityBase<Guid>, IEntity<Guid>, ITimeTrackable
    {
        private readonly List<Order> _items = new List<Order>();

        private readonly List<OrderStatus> _statuses = new List<OrderStatus>();

        public DateTime CreationDate { get; private set; }

        public DateTime? UpdateDate { get; private set; }

        public Order()
        {

        }
    }
}
