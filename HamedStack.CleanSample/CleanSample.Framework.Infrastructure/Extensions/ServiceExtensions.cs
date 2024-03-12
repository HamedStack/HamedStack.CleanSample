// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo

using CleanSample.Framework.Domain.AggregateRoots;
using CleanSample.Framework.Domain.Repositories;
using CleanSample.Framework.Infrastructure.Outbox;
using CleanSample.Framework.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CleanSample.Framework.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructureFramework<T>(this IServiceCollection services)
        where T : DbContextBase
    {

        services.AddSingleton(TimeProvider.System);
        services.AddScoped<T>();
        services.AddScoped<DbContextBase>(provider => provider.GetRequiredService<T>());
        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<T>());
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddHostedService<OutboxBackgroundService>();

        return services;
    }
}