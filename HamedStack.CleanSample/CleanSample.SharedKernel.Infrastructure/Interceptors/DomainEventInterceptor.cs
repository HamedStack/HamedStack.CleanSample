using CleanSample.SharedKernel.Domain.AggregateRoots;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;

namespace CleanSample.SharedKernel.Infrastructure.Interceptors;

internal sealed class DomainEventInterceptor(IDomainEventDispatcher domainEventDispatcher) : SaveChangesInterceptor
{
    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
        CancellationToken cancellationToken = new())
    {
        var dbContext = eventData.Context;
        if (dbContext == null)
            return await base.SavedChangesAsync(eventData, result, cancellationToken);

        var output = await base.SavedChangesAsync(eventData, result, cancellationToken);
        if (dbContext.Database.CurrentTransaction?.GetDbTransaction().Connection == null) return output;


        var domainEvents = dbContext.ChangeTracker.Entries<IHaveDomainEvents>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Count != 0)
            .SelectMany(e =>
            {
                var domainEvents = e.DomainEvents;
                e.ClearDomainEvents();
                return domainEvents;
            })
            .ToList();

        await domainEventDispatcher.DispatchEventsAsync(domainEvents, cancellationToken);

        return output;
    }

    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        var dbContext = eventData.Context;
        if (dbContext == null)
            return base.SavedChanges(eventData, result);

        var output = base.SavedChanges(eventData, result);
        if (dbContext.Database.CurrentTransaction?.GetDbTransaction().Connection == null) return output;


        var entitiesWithEvents = dbContext.ChangeTracker.Entries<IHaveDomainEvents>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Count != 0)
            .ToList();

        foreach (var entity in entitiesWithEvents)
        {
            var events = entity.DomainEvents;
            entity.ClearDomainEvents();
            domainEventDispatcher.DispatchEventsAsync(events).GetAwaiter().GetResult();
        }

        return output;
    }
}