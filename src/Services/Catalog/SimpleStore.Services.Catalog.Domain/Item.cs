namespace SimpleStore.Services.Catalog.Domain
{
    public class Item
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected Item()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            //
        }

        public Item(string name, string? description)
        {
            Name = string.IsNullOrWhiteSpace(nameof(name))
                ? throw new ArgumentException("The value cannot be null, empty or whitespaces.", nameof(name))
                : name;

            Description = description;

            CreationDate = DateTime.UtcNow;
            Id = Guid.NewGuid();
        }
    }
}
