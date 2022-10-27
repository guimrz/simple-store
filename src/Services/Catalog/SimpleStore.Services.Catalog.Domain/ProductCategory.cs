using SimpleStore.Core.Entities;
using SimpleStore.Core.Extensions;

namespace SimpleStore.Services.Catalog.Domain
{
    public class ProductCategory : IEntity<Guid>
    {
        public Guid Id { get; protected set; }

        public DateTime CreationDate { get; protected set; }

        public DateTime? UpdateDate { get; set; }

        public Guid CategoryId { get; protected set; }

        public Guid ProductId { get; protected set; }

        public virtual Category? Category { get; set; }

        public virtual Product? Product { get; set; }

        protected ProductCategory()
        {
            //
        }

        public ProductCategory(Category category, Product product)
        {
            throw new NotImplementedException();
        }
    }
}