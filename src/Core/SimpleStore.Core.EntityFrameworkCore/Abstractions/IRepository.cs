using SimpleStore.Core.Entities;

namespace SimpleStore.Core.EntityFrameworkCore.Abstractions
{
    public interface IRepository<TEntity>
        where TEntity : IEntity
    {
        IQueryable<TEntity> Entities { get; }

        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
