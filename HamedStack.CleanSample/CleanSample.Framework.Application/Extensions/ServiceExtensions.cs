﻿// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo

using CleanSample.Framework.Application.Cqrs.Commands;
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

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

            return services;
        }

    }
}