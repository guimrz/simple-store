using SimpleStore.Core.Domain;
using SimpleStore.Core.Domain.Abstractions;

namespace SimpleStore.Services.Ordering.Domain
{
    public class OrderStatus : EntityBase<Guid>, IEntity<Guid>
    {
        public Status Status { get; set; }

        public string? Comment { get; set; }

        public DateTime Date { get; protected set; }

        public OrderStatus(Status status, string? comment)
            : this(status, DateTime.UtcNow, comment)
        {
            //
        }

        public OrderStatus(Status status, DateTime date, string? comment)
        {
            Status = status;
            Date = date;
            Comment = comment;
        }
    }
}
