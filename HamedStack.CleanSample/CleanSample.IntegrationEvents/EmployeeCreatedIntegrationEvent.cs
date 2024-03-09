using CleanSample.Framework.Domain.AggregateRoots;

namespace CleanSample.IntegrationEvents;

public class EmployeeCreatedIntegrationEvent : IntegrationEvent
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}