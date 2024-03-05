using CleanSample.Framework.Application.Cqrs.Commands;

namespace CleanSample.Application.Commands;

public class CreateEmployeeCommand : ICommand<int>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required int Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public required string Email { get; set; }
}