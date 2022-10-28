using SimpleStore.Services.Ordering.Domain.Exceptions;

namespace SimpleStore.Services.Ordering.Domain
{
    public class Status
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public Status(int id, string name)
        {
            if (id <= 0)
            {
                throw new OrderingDomainException($"The value of {nameof(Id)} cannot be negative or zero.");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new OrderingDomainException($"The value of {nameof(Name)} cannot be null, empty or whitespaces.");
            }

            Id = id;
            Name = name;
        }
    }
}