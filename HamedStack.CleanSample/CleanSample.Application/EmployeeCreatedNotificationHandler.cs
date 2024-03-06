using CleanSample.Domain.AggregateRoots.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanSample.Application
{
    public class EmployeeCreatedNotificationHandler : INotificationHandler<EmployeeCreated>
    {
        private readonly ILogger<EmployeeCreatedNotificationHandler> _logger;

        public EmployeeCreatedNotificationHandler(ILogger<EmployeeCreatedNotificationHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(EmployeeCreated notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("EmployeeCreatedNotificationHandler called.");
            return Task.CompletedTask;
        }
    }
}
