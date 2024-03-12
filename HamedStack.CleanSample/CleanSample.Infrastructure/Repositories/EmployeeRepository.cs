using CleanSample.Framework.Infrastructure.Repositories;

namespace CleanSample.Infrastructure.Repositories;

public class EmployeeRepository : Repository<EmployeeRepository>
{
    public EmployeeRepository(DbContextBase identityDbContext, TimeProvider timeProvider) : base(identityDbContext, timeProvider)
    {
    }
}