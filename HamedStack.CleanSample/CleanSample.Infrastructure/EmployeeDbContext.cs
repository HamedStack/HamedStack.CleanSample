using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;
using CleanSample.Domain.AggregateRoots;
using CleanSample.Framework.Infrastructure.Repositories;

namespace CleanSample.Infrastructure;

public class EmployeeDbContext : DbContextBase
{
    public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options, ILogger<EmployeeDbContext> logger) : base(options, logger)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }
}