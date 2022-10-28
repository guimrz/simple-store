using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using SimpleStore.Core.Domain.Abstractions;
using SimpleStore.Core.EntityFrameworkCore.Abstractions;
using SimpleStore.Core.EntityFrameworkCore.Interceptors;

namespace SimpleStore.Core.EntityFrameworkCore
{
    public class UnitOfWork<TContext> : IUnitOfWork, IDisposable
        where TContext : DbContext
    {
        private bool disposedValue;

        protected readonly DbContext dbContext;
        protected readonly IServiceProvider serviceProvider;

        public UnitOfWork(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            dbContext = serviceProvider.GetRequiredService<TContext>();
        }

        public IRepository<T> Repository<T>()
            where T : class, IEntity
        {
            Type repositoryType = typeof(Repository<>).MakeGenericType(typeof(T));
            var repository = Activator.CreateInstance(repositoryType, dbContext) as Repository<T>;

            return repository!;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            var entries = dbContext.ChangeTracker.Entries();

            ExecuteInterceptors(entries);          
            var domainEvents = ReadDomainEvents(entries);

            await dbContext.SaveChangesAsync();
            await DispatchDomainEvents(domainEvents);            
        }

        protected void ExecuteInterceptors(IEnumerable<EntityEntry> entries)
        {
            var interceptors = serviceProvider.GetServices<IChangeTrackerInterceptor>();
            foreach (var changedEntity in entries)
            {
                foreach (var interceptor in interceptors)
                {
                    interceptor.Intercept(changedEntity);
                }
            }
        }

        protected IEnumerable<INotification> ReadDomainEvents(IEnumerable<EntityEntry> entries)
        {
            List<INotification> notifications = new List<INotification>();
            foreach (var entry in entries.Where(e => e.Entity is IEntity).Select(e => e.Entity).Cast<IEntity>())
            {
                notifications.AddRange(entry.Events);
            }

            return notifications;
        }

        protected Task DispatchDomainEvents(IEnumerable<INotification> domainEvents)
        {
            var mediator = serviceProvider.GetRequiredService<IMediator>();

            foreach(var domainEvent in domainEvents)
            {
                mediator.Publish(domainEvent).ConfigureAwait(false);
            }

            return Task.CompletedTask;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                dbContext.Dispose();

                disposedValue = true;
            }
        }

        ~UnitOfWork()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}