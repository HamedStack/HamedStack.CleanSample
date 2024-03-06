using System.Text.Json;
using System.Text.RegularExpressions;
using CleanSample.Framework.Domain.AggregateRoots;
using CleanSample.Framework.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CleanSample.Framework.Infrastructure.Outbox;

public class OutboxBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public OutboxBackgroundService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DbContextBase>();

                var messages = await dbContext.OutboxMessages
                    .Where(m => !m.IsProcessed)
                    .ToListAsync(stoppingToken);

                foreach (var message in messages)
                {
                    //var eventKey = new Regex("\"EventKey\":\"(.+?)\"").Match(message.Content).Groups[1].ToString();

                    //var eventType = AppDomain.CurrentDomain.GetAssemblies()
                    //    .SelectMany(x => x.GetTypes())
                    //    .FirstOrDefault(x => x.AssemblyQualifiedName == eventKey);
                    /*
                    var domainEvent = JsonSerializer.Deserialize(message.Content, eventType!)!;
                    var dispatcher = scope.ServiceProvider.GetRequiredService<IDomainEventDispatcher>();
                    await dispatcher.DispatchEventAsync(domainEvent, stoppingToken);

                    message.IsProcessed = true;
                    message.ProcessedOn = DateTimeOffset.Now;*/
                }
                await dbContext.SaveChangesAsync(stoppingToken);
            }
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}
