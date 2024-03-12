using CleanSample.Domain.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanSample.Framework.Application.Cqrs.PipelineBehaviors;

public sealed class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResult
{
    private readonly ILogger<TRequest> _logger;

    public LoggingBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        _logger.LogInformation("Processing request {RequestName}", requestName);
        var result = await next();
        if (result.IsSuccess)
        {
            _logger.LogInformation("Completed request {RequestName}", requestName);
        }
        else
        {
            _logger.LogError("Completed request {RequestName} with error", requestName);
        }
        return result;
    }
}