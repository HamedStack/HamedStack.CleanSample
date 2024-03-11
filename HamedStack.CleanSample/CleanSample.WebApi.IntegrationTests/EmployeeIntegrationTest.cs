using CleanSample.Application.Commands;
using CleanSample.WebApi.Endpoints;
using FluentAssertions;

namespace CleanSample.WebApi.IntegrationTests;

public class EmployeeIntegrationTest : WebIntegrationTestBase
{
    public EmployeeIntegrationTest(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public void ShouldSeedDataSuccessfully()
    {
        var employees = DbContext.Employees.ToList();
        var count = employees.Count;

        count.Should().Be(20);
    }

    [Fact]
    public async Task ShouldInsertDataIntoDatabase()
    {
        var createEmployeeCommand = new CreateEmployeeCommand
        {
            Email = "hamedfathi@example.com",
            BirthDate = new DateTime(1988, 1, 1),
            Gender = 1,
            FirstName = "hamed",
            LastName = "fathi"
        };
        var result = await CreateEmployeeEndpoint.CreateEmployeeEndpointHandler(createEmployeeCommand, Dispatcher);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeGreaterThan(20);
    }

}