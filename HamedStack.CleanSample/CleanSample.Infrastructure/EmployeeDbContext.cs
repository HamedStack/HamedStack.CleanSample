using CleanSample.SharedKernel.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;
using CleanSample.Domain.AggregateRoots;

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