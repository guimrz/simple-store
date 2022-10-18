using Microsoft.EntityFrameworkCore;
using SimpleStore.Core.Entities;
using SimpleStore.Services.Catalog.Repository.TypeConfigurations;

namespace SimpleStore.Services.Catalog.Repository
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
            //
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            DateTime date = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified))
            {
                if (entry.Entity is IEntity)
                {
                    (entry.Entity as IEntity)!.UpdateDate = date;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BrandTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ItemTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}