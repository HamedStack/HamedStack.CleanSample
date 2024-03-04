using System.ComponentModel.DataAnnotations.Schema;
using CleanSample.SharedKernel.Domain.Entities;

namespace CleanSample.SharedKernel.Domain.AggregateRoots;

/// <summary>
/// Represents the root of an aggregate, a cluster of domain objects that are treated as a single unit 
/// for data changes. The Aggregate Root guarantees the consistency of changes being made within the aggregate 
/// by forbidding external objects from holding references to its members.
/// </summary>
public class AggregateRoot<TId> : Entity<TId>, IHaveDomainEvents
    where TId : notnull
{
    protected AggregateRoot(TId id) : base(id)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{T}"/> class without setting an identifier.
    /// </summary>
    protected AggregateRoot()
    {
    }

    private readonly List<DomainEvent> _domainEvents = new();
    /// <summary>
    /// Gets an immutable collection of domain events associated with the entity.
    /// </summary>
    /// <value>
    /// An immutable collection of domain events.
    /// </value>
    /// <remarks>
    /// Use this property to iterate through the domain events without modifying the underlying collection.
    /// </remarks>
    [NotMapped]
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Adds a domain event to the current entity's list of domain events.
    /// </summary>
    /// <param name="newEvent">The domain event to add.</param>
    /// <remarks>
    /// Domain events are typically used in domain-driven design to capture 
    /// side effects of actions on entities that need to be propagated 
    /// outside the domain layer.
    /// </remarks>
    public virtual void AddDomainEvent(DomainEvent newEvent)
    {
        _domainEvents.Add(newEvent);
    }

    /// <summary>
    /// Removes a domain event from the current entity's list of domain events.
    /// </summary>
    /// <param name="eventItem">The domain event to remove.</param>
    /// <remarks>
    /// If the specified event is not in the list, this method will have no effect.
    /// </remarks>
    public virtual void RemoveDomainEvent(DomainEvent eventItem)
    {
        _domainEvents.Remove(eventItem);
    }

    /// <summary>
    /// Clears all domain events from the current entity's list.
    /// </summary>
    /// <remarks>
    /// Use this method with caution as it will remove all pending events 
    /// that might not have been dispatched yet.
    /// </remarks>
    public virtual void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}