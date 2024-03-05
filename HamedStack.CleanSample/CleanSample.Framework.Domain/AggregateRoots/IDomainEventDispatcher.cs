namespace CleanSample.Framework.Domain.AggregateRoots;

public interface IDomainEventDispatcher
{
    Task DispatchEventsAsync(IEnumerable<DomainEvent> domainEvents, CancellationToken cancellationToken = default);
    Task DispatchEventAsync(DomainEvent domainEvent, CancellationToken cancellationToken = default);
}