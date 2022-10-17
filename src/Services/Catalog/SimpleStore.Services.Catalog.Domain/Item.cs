using SimpleStore.Core.Entities;

namespace SimpleStore.Services.Catalog.Domain
{
    public class Item : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public Brand Brand { get; set; }

        public Guid BrandId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <exception cref="System.ArgumentException">The value cannot be null, empty or whitespaces. - name</exception>
        public Item(string name, string? description, Brand brand)
        {
            Name = string.IsNullOrWhiteSpace(nameof(name))
                ? throw new ArgumentException("The value cannot be null, empty or whitespaces.", nameof(name))
                : name;

            Brand = brand ?? throw new ArgumentNullException(nameof(brand));

            Description = description;

            CreationDate = DateTime.UtcNow;
            Id = Guid.NewGuid();
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.        
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        protected Item()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            //
        }
    }
}
