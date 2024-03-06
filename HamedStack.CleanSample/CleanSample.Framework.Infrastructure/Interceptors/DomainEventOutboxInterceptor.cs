﻿using System.Text.Json;
using CleanSample.Framework.Domain.AggregateRoots;
using CleanSample.Framework.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CleanSample.Framework.Infrastructure.Interceptors;

internal class DomainEventOutboxInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
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
                var domainEvents = e.DomainEvents.ToList();
                e.ClearDomainEvents();
                return domainEvents;
            })
            .ToList();

        var outboxMessages = domainEvents.Select(domainEvent => new OutboxMessage()
        {
            Id = Guid.NewGuid(),
            Name = domainEvent.GetType().Name,
            Content = JsonSerializer.Serialize(domainEvent),
            CreatedOn = DateTimeOffset.Now,
            IsProcessed = false,
            ProcessedOn = null,
        }).ToList();

        if (outboxMessages.Count > 0)
            dbContext.Set<OutboxMessage>().AddRange(outboxMessages);
    }
}