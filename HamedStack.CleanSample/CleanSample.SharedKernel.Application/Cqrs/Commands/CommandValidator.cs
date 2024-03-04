using FluentValidation;

namespace CleanSample.SharedKernel.Application.Cqrs.Commands;

public abstract class CommandValidator<TCommand, TCommandResult> : AbstractValidator<TCommand>
    where TCommand : ICommand<TCommandResult>
{
}