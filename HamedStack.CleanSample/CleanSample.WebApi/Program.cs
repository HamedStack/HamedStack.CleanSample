using CleanSample.Infrastructure;
using CleanSample.WebApi.REPR;
using Microsoft.EntityFrameworkCore;
using CleanSample.Framework.Application.Extensions;
using CleanSample.Framework.Infrastructure.Extensions;
using FluentValidation;
using CleanSample.Application.Commands.Handlers;
using CleanSample.WebApi.Handlers;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGlobalExceptionHandler();

builder.Services.AddInfrastructureFramework<EmployeeDbContext>();

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseGlobalExceptionHandler();

app.UseAuthorization();

app.MapControllers();
app.MapMinimalApiEndpoints();

app.Run();
