using CleanSample.Domain.AggregateRoots;
using CleanSample.Domain.Enumerations;
using CleanSample.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CleanSample.Infrastructure;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var dbContext = new EmployeeDbContext(serviceProvider.GetRequiredService<DbContextOptions<EmployeeDbContext>>()
            , serviceProvider.GetRequiredService<ILogger<EmployeeDbContext>>());

        if (dbContext.Employees.Any())
        {
            return;
        }

        PopulateTestData(dbContext);
    }

    private static void PopulateTestData(EmployeeDbContext dbContext)
    {
        foreach (var employee in dbContext.Employees)
        {
            dbContext.Remove(employee);
        }

        dbContext.SaveChanges();

        dbContext.Employees.AddRange(GetEmployees());

        dbContext.SaveChanges();
    }

    private static IEnumerable<Employee> GetEmployees()
    {
        var employees = new List<Employee>()
        {
            new Employee
            {
                Id = 1,
                FullName = new FullName("Jane", "Doe"),
                Gender = Gender.Female,
                Title = new Title("CEO"),
                BirthDate = new DateTime(1975, 5, 20),
                HireDate = new DateTime(2000, 1, 10),
                Email = new Email("jane.doe@example.com"),
                EmployeeStatus = EmployeeStatus.Active,
                Phone = new Phone("1234567890"),
            },
            new Employee
            {
                Id = 2,
                FullName = new FullName("John", "Smith"),
                Gender = Gender.Male,
                Title = new Title("CTO"),
                BirthDate = new DateTime(1980, 8, 15),
                HireDate = new DateTime(2005, 3, 1),
                Email = new Email("john.smith@example.com"),
                EmployeeStatus = EmployeeStatus.Active,
                ReportsTo = 1,
            },
            new Employee
            {
                Id = 3,
                FullName = new FullName("Emily", "Jones"),
                Gender = Gender.Female,
                Title = new Title("CFO"),
                BirthDate = new DateTime(1982, 11, 5),
                HireDate = new DateTime(2006, 7, 12),
                Email = new Email("emily.jones@example.com"),
                EmployeeStatus = EmployeeStatus.Active,
                ReportsTo = 1,
            }
        };

        return employees;
    }
}