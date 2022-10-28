using SimpleStore.Core.Domain;
using SimpleStore.Core.Domain.Abstractions;
using SimpleStore.Core.Extensions;
using SimpleStore.Services.Catalog.Domain.Exceptions;

namespace SimpleStore.Services.Catalog.Domain
{
    public class Category : EntityBase<Guid>, IEntity<Guid>
    {
        private readonly List<Product> _products = new List<Product>();
        private string _name = default!;

        public string Name
        {
            get => _name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new CatalogDomainException($"The value of {nameof(Name)} cannot be null, empty or whitespaces.");
                }

                _name = value;
            }
        }

        public string? Description { get; set; }

        public Category(string name, string? description)
        {
            Name = name;
            Description = description;
        }
    }
}