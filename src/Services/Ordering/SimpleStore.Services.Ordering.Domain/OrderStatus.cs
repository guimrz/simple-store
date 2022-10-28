using SimpleStore.Services.Ordering.Domain.Exceptions;

namespace SimpleStore.Services.Ordering.Domain
{
    public class OrderStatus
    {
        public Guid Id { get; private set; }

        public Status Status { get; private set; }

        public string? Comment { get; private set; }

        public DateTime Date { get; private set; }

        public OrderStatus(Status status, string? comment)
        {
            if (status is null)
            {
                throw new OrderingDomainException($"The value of {Status} cannot be null.");
            }

            Id = Guid.NewGuid();
            Status = status;
            Comment = comment;
            Date = DateTime.UtcNow;
        }

        #region EFCore
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected OrderStatus()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            //
        }
        #endregion
    }
}
