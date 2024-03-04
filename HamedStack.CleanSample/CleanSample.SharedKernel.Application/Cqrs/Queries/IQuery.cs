// ReSharper disable UnusedTypeParameter

using CleanSample.SharedKernel.Application.Results;
using MediatR;

namespace CleanSample.SharedKernel.Application.Cqrs.Queries;

public interface IQuery<TResult> : IRequest<Result<TResult>>
{
}