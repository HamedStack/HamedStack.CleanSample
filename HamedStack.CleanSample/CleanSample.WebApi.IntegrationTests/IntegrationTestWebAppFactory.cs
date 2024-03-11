using Microsoft.AspNetCore.Mvc.Testing;
using CleanSample.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Sqlite;

namespace CleanSample.WebApi.IntegrationTests;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>
{
    private SqliteConnection? _connection;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        _connection.Open();

        builder.ConfigureTestServices(services =>
        {
            services.AddDbContext<EmployeeDbContext>(options => { options.UseSqlite(_connection); });

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<EmployeeDbContext>();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            SeedData.Initialize(scope.ServiceProvider);
        });
    }
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (!disposing) return;
        _connection?.Close();
        _connection?.Dispose();
    }
}