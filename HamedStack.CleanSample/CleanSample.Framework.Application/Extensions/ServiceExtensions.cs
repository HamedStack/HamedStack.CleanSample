// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo

using CleanSample.Framework.Application.Cqrs.Commands;
using CleanSample.Framework.Application.Cqrs.Dispatchers;
using CleanSample.Framework.Application.Cqrs.PipelineBehaviors;
using CleanSample.Framework.Application.Cqrs.Queries;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanSample.Framework.Application.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationFramework(this IServiceCollection services)
    {
        var commandValidatorsAssemblies = AssemblyExtensions.GetAllAppDomainAssemblies().FindAssembliesWithImplementationsOf(typeof(CommandValidator<,>));
        var queryValidatorsAssemblies = AssemblyExtensions.GetAllAppDomainAssemblies().FindAssembliesWithImplementationsOf(typeof(QueryValidator<,>));

        var validatorsAssemblies = commandValidatorsAssemblies.Concat(queryValidatorsAssemblies).ToList();
        if (validatorsAssemblies.Any())
        {
            services.AddValidatorsFromAssemblies(validatorsAssemblies);
        }

        var allTypes = new[]
        {
            typeof(IMediator),
            typeof(IQuery<>),
            typeof(ICommand<>),
            typeof(IQueryHandler<,>),
            typeof(ICommandHandler<,>)
        };

        var appDomain = AppDomain.CurrentDomain.GetAssemblies();

        var assemblies1 = AssemblyExtensions.AppDomainContains(allTypes);
        var assemblies2 = appDomain.FindAssembliesWithImplementationsOf(typeof(ICommand<>));
        var assemblies3 = appDomain.FindAssembliesWithImplementationsOf(typeof(IQuery<>));

        var assemblies = assemblies1.Concat(assemblies2).Concat(assemblies3).ToArray();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
        services.AddScoped<ICommandQueryDispatcher, CommandQueryDispatcher>();

        return services;
    }

}