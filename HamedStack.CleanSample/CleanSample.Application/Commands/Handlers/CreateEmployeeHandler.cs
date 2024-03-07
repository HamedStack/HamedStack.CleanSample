using CleanSample.Domain.AggregateRoots;
using CleanSample.Domain.AggregateRoots.DomainEvents;
using CleanSample.Domain.Enumerations;
using CleanSample.Domain.ValueObjects;
using CleanSample.Framework.Application.Cqrs.Commands;
using CleanSample.Framework.Application.Results;
using CleanSample.Framework.Domain.Repositories;

namespace CleanSample.Application.Commands.Handlers;

public class CreateEmployeeHandler : ICommandHandler<CreateEmployeeCommand, int>
{
    private readonly IRepository<Employee> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateEmployeeHandler(IRepository<Employee> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {

        var employee = new Employee()
        {
            FullName = new FullName(request.FirstName, request.LastName),
            Gender = Gender.FromValue(request.Gender),
            Email = new Email(request.Email),
            BirthDate = request.BirthDate
        };


        employee.AddDomainEvent(new EmployeeCreatedDomainEvent() { FirstName = request.FirstName, LastName = request.LastName });

        var result = await _repository.AddAsync(employee, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result.Id;
    }
}