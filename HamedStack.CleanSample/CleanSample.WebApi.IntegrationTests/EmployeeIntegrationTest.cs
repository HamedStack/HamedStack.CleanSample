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
}