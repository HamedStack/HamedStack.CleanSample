using CleanSample.Application.Commands;
using CleanSample.Framework.Application.Cqrs.Commands;
using FluentValidation;

namespace CleanSample.Application.Handlers
{
    public class CreateEmployeeValidator : CommandValidator<CreateEmployeeCommand,int>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(e => e.Gender).InclusiveBetween(1, 2);
        }
    }
}
