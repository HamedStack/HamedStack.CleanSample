using FluentValidation;
using MediatR;

namespace CleanSample.SharedKernel.Application.Cqrs.Queries;

public abstract class QueryValidator<TQuery, TQueryResult> : AbstractValidator<TQuery>
    where TQuery : IRequest<TQueryResult>
{
}