using CleanSample.Framework.Application.Cqrs.Dispatchers;
using CleanSample.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace CleanSample.WebApi.IntegrationTests;

public abstract class WebIntegrationTestBase : IClassFixture<IntegrationTestWebAppFactory>
{
    public HttpClient HttpClient { get; }
    protected ICommandQueryDispatcher Dispatcher { get; }
    protected EmployeeDbContext DbContext { get; }

    protected WebIntegrationTestBase(IntegrationTestWebAppFactory factory)
    {
        var scope = factory.Services.CreateScope();
        Dispatcher = scope.ServiceProvider.GetRequiredService<ICommandQueryDispatcher>();
        DbContext = scope.ServiceProvider.GetRequiredService<EmployeeDbContext>();
        HttpClient = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>().CreateClient();
    }
}