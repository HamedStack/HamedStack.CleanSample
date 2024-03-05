using CleanSample.Domain.Enumerations;
using CleanSample.Domain.ValueObjects;
using CleanSample.SharedKernel.Application.Cqrs.Commands;

namespace CleanSample.Application.Commands;

public class CreateEmployeeCommand : ICommand<int>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required Gender Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public required Email Email { get; set; }
}