using System.Diagnostics;
using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanSample.Framework.Application.Cqrs.PipelineBehaviors;

public sealed class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private const long SlowRequestThreshold = 1000; // milliseconds

    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;

    public PerformanceBehaviour(ILogger<TRequest> logger)
    {
        _timer = new Stopwatch();
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next().ConfigureAwait(false);

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds <= SlowRequestThreshold) return response;

        var requestName = typeof(TRequest).Name;
        var jsonObj = JsonSerializer.Serialize(request, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        _logger.LogWarning($"Performance Long Running Request: {requestName} {elapsedMilliseconds} millisecond(s). {jsonObj}");

        return response;
    }
}