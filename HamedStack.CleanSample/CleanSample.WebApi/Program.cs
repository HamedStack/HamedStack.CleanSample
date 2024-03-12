using CleanSample.Framework.Application.Extensions;
using CleanSample.Framework.Infrastructure.Extensions;
using CleanSample.Framework.Infrastructure.Identity;
using CleanSample.Framework.Infrastructure.Identity.DynamicPermissions;
using CleanSample.Infrastructure;
using CleanSample.WebApi.Handlers;
using CleanSample.WebApi.REPR;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGlobalExceptionHandler();

builder.Services.AddInfrastructureFramework<EmployeeDbContext, ApplicationUser, IdentityRole>();

builder.Services.AddDbContext<EmployeeDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("EmployeeDb") ?? "Data Source=database.db"));

builder.Services.AddApplicationFramework();

builder.Services.AddMinimalApiEndpoints();

builder.Services.AddMassTransit(x =>
{
    x.UsingInMemory((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddDynamicPermission(opt => opt.DisableCache = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseGlobalExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapMinimalApiEndpoints();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<EmployeeDbContext>();
        context.Database.Migrate();
        context.Database.EnsureCreated();
        SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<CleanSample.WebApi.Program>>();
        logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
    }
}

app.Run();


// Exposing for integration test.
namespace CleanSample.WebApi
{
    public partial class Program
    {
    }
}