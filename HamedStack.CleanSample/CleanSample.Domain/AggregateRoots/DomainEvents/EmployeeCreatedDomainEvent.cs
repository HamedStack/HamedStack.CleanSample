using CleanSample.Framework.Domain.AggregateRoots;

namespace CleanSample.Domain.AggregateRoots.DomainEvents;

public class EmployeeCreatedDomainEvent : DomainEvent
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }

}