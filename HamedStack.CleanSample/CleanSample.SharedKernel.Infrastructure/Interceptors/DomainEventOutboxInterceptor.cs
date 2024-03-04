using System.Text.Json;
using CleanSample.SharedKernel.Domain.AggregateRoots;
using CleanSample.SharedKernel.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CleanSample.SharedKernel.Infrastructure.Interceptors;

internal class DomainEventOutboxInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (eventData.Context != null)
        {
            InsertOutboxMessages(eventData.Context);
        }
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void InsertOutboxMessages(DbContext dbContext)
    {
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


        var outboxMessages = domainEvents.Select(d => new OutboxMessage()
        {
            Id = Guid.NewGuid(),
            Name = d.GetType().FullName ?? d.GetType().Name,
            Content = JsonSerializer.Serialize(d),
            CreatedOn = DateTimeOffset.Now,
            IsProcessed = false,
            ProcessedOn = null,
        }).ToList();

        dbContext.Set<OutboxMessage>().AddRange(outboxMessages);
    }
}