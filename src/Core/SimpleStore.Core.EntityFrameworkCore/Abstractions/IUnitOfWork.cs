using SimpleStore.Core.Domain.Abstractions;

namespace SimpleStore.Core.EntityFrameworkCore.Abstractions
{
    public interface IUnitOfWork
    {
        IRepository<T> Repository<T>() where T : class, IEntity;

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
