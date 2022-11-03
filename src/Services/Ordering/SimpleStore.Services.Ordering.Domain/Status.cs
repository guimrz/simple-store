using SimpleStore.Core.Domain;
using SimpleStore.Core.Domain.Abstractions;
using SimpleStore.Services.Ordering.Domain.Exceptions;

namespace SimpleStore.Services.Ordering.Domain
{
    public class Status : EntityBase<int>, IEntity<int>
    {
        private string _name = default!;

        public string Name
        {
            get => _name;
            protected set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new OrderingDomainException($"The value of {nameof(Name)} cannot be null, empty or whitespaces.");

                }

                _name = value;
            }
        }

        public Status(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}