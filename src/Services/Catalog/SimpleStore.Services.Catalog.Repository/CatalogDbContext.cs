using Microsoft.EntityFrameworkCore;
using SimpleStore.Services.Catalog.Repository.TypeConfigurations;

namespace SimpleStore.Services.Catalog.Repository
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
            //
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BrandTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ItemTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }    
}
