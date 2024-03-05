using CleanSample.Application.Commands;
using CleanSample.Domain.AggregateRoots;
using CleanSample.Domain.Enumerations;
using CleanSample.Domain.ValueObjects;
using CleanSample.SharedKernel.Application.Cqrs.Commands;
using CleanSample.SharedKernel.Application.Results;
using CleanSample.SharedKernel.Domain.Repositories;

namespace CleanSample.Application.Handlers;

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
        var result = await _repository.AddAsync(employee, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result.Id;
    }
}