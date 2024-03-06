using CleanSample.Domain.AggregateRoots.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanSample.Application.NotificationHandlers
{
    public class EmployeeCreatedNotificationHandler : INotificationHandler<EmployeeCreatedDomainEvent>
    {
        private readonly ILogger<EmployeeCreatedNotificationHandler> _logger;

        public EmployeeCreatedNotificationHandler(ILogger<EmployeeCreatedNotificationHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(EmployeeCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("EmployeeCreatedNotificationHandler called.");
            return Task.CompletedTask;
        }
    }
}
