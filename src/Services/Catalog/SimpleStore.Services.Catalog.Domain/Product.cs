using SimpleStore.Core.Entities;
using SimpleStore.Core.Extensions;

namespace SimpleStore.Services.Catalog.Domain
{
    public class Product : IEntity<Guid>
    {
        private string _name = default!;
        private List<ProductCategory> _categories = new List<ProductCategory>();

        public Guid Id { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                 value.ThrowIfNullOrWhitespaces(nameof(Name));
                _name = value;
            }
        }

        public string? Description { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public Guid BrandId { get; protected set; }

        public virtual Brand? Brand { get; set; }

        public IReadOnlyCollection<ProductCategory> Categories 
        {
            get => _categories;
        }

        public Product(string name, string? description)
        {
            Name = name;
            Description = description;

            CreationDate = DateTime.UtcNow;
            Id = Guid.NewGuid();
        }
    }
}
