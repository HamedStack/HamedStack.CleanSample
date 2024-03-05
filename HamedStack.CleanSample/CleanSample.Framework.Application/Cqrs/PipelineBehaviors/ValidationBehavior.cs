using CleanSample.Framework.Application.Cqrs.Commands;
using FluentValidation;
using MediatR;

namespace CleanSample.Framework.Application.Cqrs.PipelineBehaviors;

public sealed class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, ICommand<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationFailures = await Task.WhenAll(
            _validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

        var errors = validationFailures
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)
            .ToList();

        if (errors.Count != 0)
        {
            var finalError = errors
                .Select(e => e.ErrorMessage)
                .Aggregate((a, b) => a + Environment.NewLine + b);
            throw new Exception(finalError);

        }

        var response = await next();

        return response;
    }
}