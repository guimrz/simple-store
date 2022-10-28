using MediatR;

namespace SimpleStore.Core.Domain.Abstractions
{
    public interface IEntity
    {
        IReadOnlyCollection<INotification> Events { get; }

        void Add(INotification notification);

        void Remove(INotification notification);

        void Clear();
    }

    public interface IEntity<TIdentifier> : IEntity
    {
        TIdentifier Id { get; }
    }
}
