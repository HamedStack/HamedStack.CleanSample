// ReSharper disable UnusedTypeParameter
// ReSharper disable UnusedMember.Global

using CleanSample.SharedKernel.Application.Results;
using MediatR;

namespace CleanSample.SharedKernel.Application.Cqrs.Commands;


public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, Result<TResult>>
    where TCommand : ICommand<TResult>
{
}