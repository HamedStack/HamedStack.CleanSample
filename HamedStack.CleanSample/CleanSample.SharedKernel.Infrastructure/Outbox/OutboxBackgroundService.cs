using System.Text.Json;
using CleanSample.SharedKernel.Domain.AggregateRoots;
using CleanSample.SharedKernel.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace CleanSample.SharedKernel.Infrastructure.Outbox;

public class OutboxBackgroundService(DbContextBase dbContext, IDomainEventDispatcher dispatcher) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var messages = await dbContext.OutboxMessages
                .Where(m => !m.IsProcessed)
                .ToListAsync(stoppingToken);

            foreach (var message in messages)
            {

                var domainEvent = JsonSerializer.Deserialize<DomainEvent>(message.Content)!;
                await dispatcher.DispatchEventAsync(domainEvent, stoppingToken);

                message.IsProcessed = true;
                message.ProcessedOn = DateTimeOffset.Now;

            }
            await dbContext.SaveChangesAsync(stoppingToken);
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}