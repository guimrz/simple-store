using SimpleStore.Core.Domain;
using SimpleStore.Core.Domain.Abstractions;
using SimpleStore.Core.Extensions;
using SimpleStore.Services.Catalog.Domain.Exceptions;

namespace SimpleStore.Services.Catalog.Domain
{
    public class Brand : EntityBase<Guid>, IEntity<Guid>, ITimeTrackable
    {
        private string _name = default!;

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

        public DateTime CreationDate { get; private set; }

        public DateTime? UpdateDate { get; private set; }

        public Brand(string name, string? description)
        {
            Name = name;
            Description = description;
        }
    }
}