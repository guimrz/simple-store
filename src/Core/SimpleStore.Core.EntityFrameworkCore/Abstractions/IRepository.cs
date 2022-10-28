using SimpleStore.Core.Domain.Abstractions;

namespace SimpleStore.Core.EntityFrameworkCore.Abstractions
{
    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        IQueryable<TEntity> Entities { get; }

        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
