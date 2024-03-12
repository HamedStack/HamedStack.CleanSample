using CleanSample.Domain.AggregateRoots;
using CleanSample.Domain.AggregateRoots.DomainEvents;
using CleanSample.Domain.Enumerations;
using CleanSample.Domain.ValueObjects;
using CleanSample.Framework.Application.Cqrs.Commands;
using CleanSample.Framework.Domain.Repositories;
using CleanSample.Framework.Domain.Results;
using CleanSample.IntegrationEvents;
using MassTransit;

namespace CleanSample.Application.Commands.Handlers;

public class CreateEmployeeHandler : ICommandHandler<CreateEmployeeCommand, int>
{
    private readonly IRepository<Employee> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublishEndpoint _publishEndpoint;


    public CreateEmployeeHandler(IRepository<Employee> repository, IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Result<int>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {

        var employee = new Employee
        {
            FullName = new FullName(request.FirstName, request.LastName),
            Gender = Gender.FromValue(request.Gender),
            Email = new Email(request.Email),
            BirthDate = request.BirthDate
        };


        employee.AddDomainEvent(new EmployeeCreatedDomainEvent { FirstName = request.FirstName, LastName = request.LastName });

        var result = await _repository.AddAsync(employee, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);


        var integrationEvent = new EmployeeCreatedIntegrationEvent
        {
            EmployeeId = result.Id,
            FirstName = request.FirstName,
            LastName = request.LastName
        };
        await _publishEndpoint.Publish(integrationEvent, cancellationToken);
        
        return result.Id;
    }
}