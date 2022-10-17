using SimpleStore.Core.Entities;

namespace SimpleStore.Core.EntityFrameworkCore.Abstractions
{
    public interface IUnitOfWork
    {
        IRepository<T> Repository<T>() where T : IEntity;

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
