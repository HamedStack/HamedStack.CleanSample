using FluentValidation;

namespace CleanSample.Framework.Application.Cqrs.Commands;

public abstract class CommandValidator<TCommand, TCommandResult> : AbstractValidator<TCommand>
    where TCommand : ICommand<TCommandResult>
{
}