// ReSharper disable UnusedTypeParameter
// ReSharper disable UnusedMember.Global

using CleanSample.Domain.Results;
using MediatR;

namespace CleanSample.Framework.Application.Cqrs.Commands;


public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, Result<TResult>>
    where TCommand : ICommand<TResult>
{
}