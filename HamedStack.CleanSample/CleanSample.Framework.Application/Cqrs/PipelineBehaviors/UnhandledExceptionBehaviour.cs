using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanSample.Framework.Application.Cqrs.PipelineBehaviors;

public sealed class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next().ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            var jsonObj = JsonSerializer.Serialize(request, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            _logger.LogError(ex, $"Exception Request: Unhandled Exception for Request {requestName} {jsonObj}");

            throw;
        }
    }
}