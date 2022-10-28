using SimpleStore.Core.Domain;
using SimpleStore.Core.Domain.Abstractions;
using SimpleStore.Services.Catalog.Domain.Events;
using SimpleStore.Services.Catalog.Domain.Exceptions;

namespace SimpleStore.Services.Catalog.Domain
{
    public class Product : EntityBase<Guid>, IEntity<Guid>, ITimeTrackable
    {
        private decimal _price;
        private int _stock;
        private string _name = default!;
        private readonly List<Category> _categories = new List<Category>();

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new CatalogDomainException($"The value of {nameof(Name)} cannot be null, empty or whitespaces.");
                }

                _name = value;
            }
        }

        public string? Description { get; set; }

        public string? PictureUrl { get; set; }

        public decimal Price
        {
            get => _price;
            protected set
            {
                if (value < 0)
                {
                    throw new CatalogDomainException($"The value of {nameof(Price)} cannot be less than zero.");
                }

                _price = value;
            }
        }

        public int Stock
        {
            get => _stock;
            protected set
            {
                if (value < 0)
                {
                    throw new CatalogDomainException($"The value of {nameof(Stock)} cannot be negative.");
                }

                _stock = value;
            }
        }

        public Brand? Brand { get; set; }

        public IReadOnlyCollection<Category> Categories
        {
            get => _categories;
        }

        public DateTime CreationDate { get; protected set; }

        public DateTime? UpdateDate { get; protected set; }

        public Product(string name, decimal price, int stock, string? description)
        {
            Name = name;
            Price = price;
            Description = description;
            Stock = stock;
        }

        public void UpdateStock(int stock)
        {
            if (Stock != stock)
            {
                Stock = stock;

                Add(new StockUpdatedDomainEvent(Id, stock));
            }
        }

        public void UpdatePrice(decimal price)
        {
            if(Price != price)
            {
                var oldPrice = Price;

                Price = price;

                Add(new PriceUpdatedDomainEvent(Id, price, oldPrice));
            }
        }

        public void Add(Category category)
        {
            if (category is null)
            {
                throw new CatalogDomainException($"The value of {nameof(category)} cannot be a null reference.");
            }

            if (_categories.Any(p => p.Id == category.Id))
            {
                throw new CatalogDomainException($"The product already contains the category with id '{category.Id}'.");
            }

            _categories.Add(category);
        }

        public void Remove(Category category)
        {
            _categories.Remove(category);
        }

        public void RemoveAll(Predicate<Category> predicate = default!)
        {
            if (predicate == default)
            {
                _categories.Clear();
            }
            else
            {
                _categories.RemoveAll(predicate);
            }
        }
    }
}