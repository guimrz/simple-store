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
            repositories.Add(typeof(Repository<Item>), new Repository<Item>(dbContext));
            repositories.Add(typeof(Repository<Brand>), new Repository<Brand>(dbContext));
        }
    }
}
