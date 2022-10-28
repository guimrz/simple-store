using MediatR;
using Microsoft.Extensions.Logging;
using SimpleStore.Services.Catalog.Domain.Events;

namespace SimpleStore.Services.Catalog.Application.Events.DomainEvents
{
    public class PriceUpdatedDomainEventHandler : INotificationHandler<PriceUpdatedDomainEvent>
    {
        private readonly ILogger _logger;

        public PriceUpdatedDomainEventHandler(ILogger<PriceUpdatedDomainEventHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Handle(PriceUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogWarning($"Handling '{nameof(PriceUpdatedDomainEventHandler)}'...");

            return Task.CompletedTask;
        }
    }
}
