// ReSharper disable UnusedTypeParameter

using CleanSample.Domain.Results;
using MediatR;

namespace CleanSample.Framework.Application.Cqrs.Queries;

public interface IQuery<TResult> : IRequest<Result<TResult>>
{
}