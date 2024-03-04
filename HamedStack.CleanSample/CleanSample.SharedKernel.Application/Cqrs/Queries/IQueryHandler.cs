// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedTypeParameter

using CleanSample.SharedKernel.Application.Results;
using MediatR;

namespace CleanSample.SharedKernel.Application.Cqrs.Queries;

public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, Result<TResult>>
    where TQuery : IQuery<TResult>
{
}