using MediatR;

namespace CleanSample.Framework.Domain.AggregateRoots;

public class DomainEventDispatcher(IMediator mediator) : IDomainEventDispatcher
{
    public async Task DispatchEventsAsync(IEnumerable<DomainEvent> domainEvents, CancellationToken cancellationToken = default)
    {
        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent, cancellationToken);
        }
    }

    public async Task DispatchEventAsync(DomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        await mediator.Publish(domainEvent, cancellationToken);
    }
}