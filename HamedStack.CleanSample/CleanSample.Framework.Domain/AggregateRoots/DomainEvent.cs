using MediatR;

namespace CleanSample.Framework.Domain.AggregateRoots;

/// <summary>
/// Represents a notification event within the domain-driven design.
/// </summary>
/// <remarks>
/// Notification events are used to communicate domain events across different parts of the application.
/// Implementing this interface allows for a standardized way to define and handle such events,
/// promoting loose coupling and enhancing the modularity of the application.
/// </remarks>
public abstract class DomainEvent : INotification
{
    public string EventType => GetType().Name;
    public DateTimeOffset DateOccurred => DateTimeOffset.UtcNow;
}


