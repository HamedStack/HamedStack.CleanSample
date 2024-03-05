using FluentValidation;
using MediatR;

namespace CleanSample.Framework.Application.Cqrs.Queries;

public abstract class QueryValidator<TQuery, TQueryResult> : AbstractValidator<TQuery>
    where TQuery : IRequest<TQueryResult>
{
}