using CleanSample.SharedKernel.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanSample.Infrastructure;

public class EmployeeDbContext : DbContextBase
{
    public EmployeeDbContext(DbContextOptions options, ILogger<DbContextBase> logger) : base(options, logger)
    {
    }
}