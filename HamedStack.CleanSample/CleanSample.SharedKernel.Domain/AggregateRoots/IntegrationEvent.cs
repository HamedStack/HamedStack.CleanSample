namespace CleanSample.SharedKernel.Domain.AggregateRoots;

// No need to implement any interface.
// It works with EventBus not MediatR.
public abstract class IntegrationEvent
{
    public Guid CorrelationId => Guid.NewGuid();
    public string EventType => GetType().FullName ?? GetType().Name;
    public DateTimeOffset DateOccurred => DateTimeOffset.UtcNow;
}