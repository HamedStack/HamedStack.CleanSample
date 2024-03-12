// ReSharper disable UnusedTypeParameter

using CleanSample.Framework.Domain.Results;
using MediatR;

namespace CleanSample.Framework.Application.Cqrs.Commands;

public interface ICommand<TResult> : IRequest<Result<TResult>>
{
}