using MediatR;
using Microsoft.Extensions.Logging;
using SimpleStore.Services.Catalog.Domain.Events;

namespace SimpleStore.Services.Catalog.Application.Events.DomainEvents
{
    public class StockUpdatedDomainEventHandler : INotificationHandler<StockUpdatedDomainEvent>
    {
        private readonly ILogger _logger;

        public StockUpdatedDomainEventHandler(ILogger<StockUpdatedDomainEventHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Handle(StockUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogWarning($"Handling '{nameof(StockUpdatedDomainEventHandler)}'...");

            return Task.CompletedTask;
        }
    }
}
