using Microsoft.EntityFrameworkCore;
using SimpleStore.Core.Entities;
using SimpleStore.Core.EntityFrameworkCore.Abstractions;

namespace SimpleStore.Core.EntityFrameworkCore
{
    public class UnitOfWork : IUnitOfWork
    {
        protected IDictionary<Type, object> repositories;
        protected readonly DbContext dbContext;

        public UnitOfWork(DbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            repositories = new Dictionary<Type, object>();
        }

        public IRepository<T> Repository<T>() where T : IEntity
        {
            var repository = repositories.SingleOrDefault(kvp => kvp.Key.IsAssignableTo(typeof(IRepository<T>))).Value;

            if (repository is null)
            {
                throw new InvalidOperationException($"The unit of work doesn't have a repository for the specified entity '{typeof(T).FullName}'-");
            }
            else if (!repository.GetType().IsAssignableTo(typeof(IRepository<T>)))
            {
                throw new InvalidOperationException($"The registered repository for the entity of type '{typeof(T).FullName}' doesn't implement the interface '{typeof(IRepository<>).FullName}'.");
            }

            return (repository as IRepository<T>)!;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
