// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo

using CleanSample.Framework.Application.Cqrs.Commands;
using CleanSample.Framework.Application.Cqrs.Dispatchers;
using CleanSample.Framework.Application.Cqrs.PipelineBehaviors;
using CleanSample.Framework.Application.Cqrs.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanSample.Framework.Application.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddFrameworkMediatR(this IServiceCollection services, params Type[] types)
        {

            var allTypes = new[]
            {
                typeof(IMediator),
                typeof(IQuery<>),
                typeof(ICommand<>),
                typeof(IQueryHandler<,>),
                typeof(ICommandHandler<,>)
            }.Concat(types).ToArray();
            
            var assemblies =
                AssemblyExtensions.AppDomainContains(allTypes).ToArray();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
            services.AddScoped<ICommandQueryDispatcher, CommandQueryDispatcher>();

            return services;
        }

    }
}
