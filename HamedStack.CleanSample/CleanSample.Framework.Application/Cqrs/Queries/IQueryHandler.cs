// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedTypeParameter

using CleanSample.Domain.Results;
using MediatR;

namespace CleanSample.Framework.Application.Cqrs.Queries;

public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, Result<TResult>>
    where TQuery : IQuery<TResult>
{
}