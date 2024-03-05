// ReSharper disable IdentifierTypo

namespace CleanSample.WebApi.REPR;

public static class MinimalApiEndpointsExtensions
{
    public static IServiceCollection AddMinimalApiEndpoints(this IServiceCollection services)
    {
        var endpointTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(IMinimalApiEndpoint).IsAssignableFrom(type) && type is { IsInterface: false, IsAbstract: false });

        foreach (var type in endpointTypes)
        {
            services.AddTransient(typeof(IMinimalApiEndpoint), type);
        }

        return services;
    }

    public static WebApplication MapMinimalApiEndpoints(this WebApplication app)
    {
        var endpoints = app.Services.GetServices<IMinimalApiEndpoint>();
        foreach (var endpoint in endpoints)
        {
            endpoint.HandleEndpoint(app);
        }

        return app;
    }
}