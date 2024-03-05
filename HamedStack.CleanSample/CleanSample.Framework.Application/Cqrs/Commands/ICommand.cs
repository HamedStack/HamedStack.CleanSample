// ReSharper disable UnusedTypeParameter

using CleanSample.Framework.Application.Results;
using MediatR;

namespace CleanSample.Framework.Application.Cqrs.Commands;

public interface ICommand<TResult> : IRequest<Result<TResult>>
{
}