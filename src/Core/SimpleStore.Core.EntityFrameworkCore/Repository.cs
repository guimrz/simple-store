using Microsoft.EntityFrameworkCore;
using SimpleStore.Core.Domain.Abstractions;
using SimpleStore.Core.EntityFrameworkCore.Abstractions;

namespace SimpleStore.Core.EntityFrameworkCore
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected DbContext dbContext;

        public IQueryable<TEntity> Entities => dbContext.Set<TEntity>();

        public Repository(DbContext dbContext)
            => this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(Repository<TEntity>.dbContext));

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
            => (await dbContext.AddAsync(entity, cancellationToken)).Entity;

        public Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
            => Task.FromResult(dbContext.Remove(entity).State == EntityState.Deleted);

        public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
            => Task.FromResult(dbContext.Update(entity).Entity);
    }
}
