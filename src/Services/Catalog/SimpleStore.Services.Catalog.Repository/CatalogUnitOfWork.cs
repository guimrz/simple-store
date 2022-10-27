using Microsoft.EntityFrameworkCore;
using SimpleStore.Core.EntityFrameworkCore;
using SimpleStore.Services.Catalog.Domain;

namespace SimpleStore.Services.Catalog.Repository
{
    public class CatalogUnitOfWork : UnitOfWork
    {
        public CatalogUnitOfWork(CatalogDbContext dbContext) 
            : base(dbContext)
        {
            repositories.Add(typeof(Repository<Product>), new Repository<Product>(dbContext));
            repositories.Add(typeof(Repository<Brand>), new Repository<Brand>(dbContext));
            repositories.Add(typeof(Repository<Category>), new Repository<Category>(dbContext));
        }
    }
}
