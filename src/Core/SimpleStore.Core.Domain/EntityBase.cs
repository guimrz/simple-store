using MediatR;
using SimpleStore.Core.Domain.Abstractions;

namespace SimpleStore.Core.Domain
{
    public class EntityBase : IEntity
    {
        protected readonly List<INotification> _events = new List<INotification>();

        public IReadOnlyCollection<INotification> Events => _events.AsReadOnly();

        public void Add(INotification notification)
        {
            if (notification is null)
            {
                throw new ArgumentNullException(nameof(notification));
            }

            _events.Add(notification);
        }

        public void Clear()
        {
            _events.Clear();
        }

        public void Remove(INotification notification)
        {
            if (notification is null)
            {
                throw new ArgumentNullException(nameof(notification));
            }

            _events.Remove(notification);
        }
    }

    public class EntityBase<TIdentifier> : EntityBase, IEntity<TIdentifier>
    {
        public TIdentifier Id { get; protected set; } = default!;
    }
}