using SimpleStore.Services.Catalog.Domain;

namespace SimpleStore.Services.Catalog.Repository.Entities
{
    public class ProductCategory
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public Guid CategoryId { get; set; }

        public virtual Product? Product { get; set; }

        public virtual Category? Category { get; set; }
    }
}
