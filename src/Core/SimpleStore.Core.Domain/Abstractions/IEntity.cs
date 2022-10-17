namespace SimpleStore.Core.Domain.Abstractions
{
    public interface IEntity<TIdentifier> : IEntity
    {
        TIdentifier Id { get; }
    }

    public interface IEntity
    {
        DateTime CreationDate { get; }

        DateTime? UpdateDate { get; }
    }
}
