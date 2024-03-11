using Bogus;
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
        dbContext.Employees.AddRange(GenerateEmployees());

        dbContext.SaveChanges();
    }

    private static IEnumerable<Employee> GetEmployees()
    {
        var employees = new List<Employee>
        {
            new()
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
            new()
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
            new()
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

    private static IEnumerable<Employee> GenerateEmployees()
    {
        var employeeTitles = new[]
        {
            "Software Engineer",
            "Project Manager",
            "Business Analyst",
            "Quality Assurance Engineer",
            "Product Manager",
            "Graphic Designer",
            "System Administrator",
            "Network Engineer",
            "UX/UI Designer",
            "Data Scientist"
        };
        var employeeIds = 4;
        var employees = new Faker<Employee>()
                .RuleFor(e => e.Id, f => employeeIds++)
                .RuleFor(e => e.FullName, f =>
                {
                    var firstName = f.Name.FirstName();
                    var lastName = f.Name.LastName();
                    return new FullName(firstName, lastName);
                })
                .RuleFor(e => e.BirthDate, f => f.Date.Between(new DateTime(1920, 1, 1), new DateTime(2000, 1, 1)))
                .RuleFor(e => e.HireDate, f => f.Date.Between(new DateTime(2020, 1, 1), DateTime.Now))
                .RuleFor(e => e.Email, f => new Email(f.Internet.Email()))
                .RuleFor(e => e.Gender, f =>
                {
                    var oneOrTwo = f.Random.Int(1, 2);
                    return Gender.FromValue(oneOrTwo);
                })
                .RuleFor(e => e.Title, f => new Title(f.PickRandom(employeeTitles)))
                .RuleFor(e => e.Phone, f => new Phone(f.Phone.PhoneNumber()))
                .RuleFor(e => e.Address, f =>
                {
                    var street = f.Address.StreetAddress();
                    var city = f.Address.City();
                    var state = f.Address.State();
                    var country = f.Address.Country();
                    var postalCode = f.Address.ZipCode();
                    return new Address(street, city, state, country, postalCode);
                })
                .RuleFor(e => e.Fax, f => new Phone(f.Phone.PhoneNumber()))
                .RuleFor(e => e.ReportsTo, f => f.Random.Int(1, 3))
                .RuleFor(e => e.EmployeeStatus, f =>
                {
                    var oneToFour = f.Random.Int(1, 4);
                    return EmployeeStatus.FromValue(oneToFour);
                })

            ;

        return employees.Generate(17);
    }
}