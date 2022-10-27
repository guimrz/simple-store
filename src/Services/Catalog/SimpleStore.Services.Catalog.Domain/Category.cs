using SimpleStore.Core.Entities;
using SimpleStore.Core.Extensions;

namespace SimpleStore.Services.Catalog.Domain
{
    public class Category : IEntity<Guid>
    {
        private string _name = default!;

        public Guid Id { get; protected set; }

        public DateTime CreationDate { get; protected set; }

        public DateTime? UpdateDate { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                value.ThrowIfNullOrWhitespaces(nameof(Name));
                _name = value;
            }
        }

        public string Description { get; set; }

        public Category(string name, string description)
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
            Name = name;
            Description = description;
        }
    }
}