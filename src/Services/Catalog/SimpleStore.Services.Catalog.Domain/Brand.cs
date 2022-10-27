﻿using SimpleStore.Core.Entities;
using SimpleStore.Core.Extensions;

namespace SimpleStore.Services.Catalog.Domain
{
    public class Brand : IEntity<Guid>
    {
        private string _name = default!;

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

        public Brand(string name, string? description)
        {
            Name = name;
            Description = description;

            CreationDate = DateTime.UtcNow;
            Id = Guid.NewGuid();
        }
    }
}