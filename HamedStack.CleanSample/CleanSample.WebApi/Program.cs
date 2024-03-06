using CleanSample.Infrastructure;
using CleanSample.WebApi.REPR;
using Microsoft.EntityFrameworkCore;
using CleanSample.Application.Handlers;
using CleanSample.Framework.Application.Extensions;
using CleanSample.Framework.Infrastructure.Extensions;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<CreateEmployeeValidator>();

builder.Services.AddFrameworkDbContext<EmployeeDbContext>();

builder.Services.AddDbContext<EmployeeDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("EmployeeDb") ?? "Data Source=database.db"));
builder.Services.AddFrameworkMediatR(typeof(CreateEmployeeHandler));

builder.Services.AddMinimalApiEndpoints();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapMinimalApiEndpoints();

app.Run();
