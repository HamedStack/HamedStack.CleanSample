// ReSharper disable UnusedTypeParameter

using CleanSample.SharedKernel.Application.Results;
using MediatR;

namespace CleanSample.SharedKernel.Application.Cqrs.Commands;

public interface ICommand<TResult> : IRequest<Result<TResult>>
{
}