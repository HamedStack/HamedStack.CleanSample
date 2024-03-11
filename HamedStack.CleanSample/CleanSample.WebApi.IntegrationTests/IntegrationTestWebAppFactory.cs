using System.Data.Common;
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
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            RemoveCurrentDescriptors(services);

            services.AddSingleton<DbConnection>(container =>
            {
                var connection = new SqliteConnection("DataSource=:memory:");
                connection.Open();

                return connection;
            });

            services.AddDbContext<EmployeeDbContext>((container, options) =>
            {
                var connection = container.GetRequiredService<DbConnection>();
                options.UseSqlite(connection);
            });

            using var scope = services.BuildServiceProvider().CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<EmployeeDbContext>();

            // db.Database.EnsureDeleted(); // For SQLite in-memory is not necessary.
            db.Database.EnsureCreated();
            SeedData.Initialize(scope.ServiceProvider);

            services.AddHttpClient();

            builder.UseEnvironment("Development");
        });
    }

    private static void RemoveCurrentDescriptors(IServiceCollection services)
    {
        var dbContextDescriptor = services.SingleOrDefault(
            d => d.ServiceType ==
                 typeof(DbContextOptions<EmployeeDbContext>));

        if (dbContextDescriptor != null) services.Remove(dbContextDescriptor);

        var dbConnectionDescriptor = services.SingleOrDefault(
            d => d.ServiceType ==
                 typeof(DbConnection));

        if (dbConnectionDescriptor != null) services.Remove(dbConnectionDescriptor);
    }
}