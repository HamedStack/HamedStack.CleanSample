using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace CleanSample.SharedKernel.Infrastructure.Interceptors;

internal class PerformanceInterceptor : DbCommandInterceptor
{
    private const long QuerySlowThreshold = 100; // milliseconds

    private readonly ILogger _logger;

    public PerformanceInterceptor(ILogger logger)
    {
        _logger = logger;
    }

    public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
    {
        if (eventData.Duration.TotalMilliseconds > QuerySlowThreshold)
        {
            LogQuery(command, eventData);
        }

        return base.ReaderExecuted(command, eventData, result);
    }

    public override ValueTask<DbDataReader> ReaderExecutedAsync(DbCommand command, CommandExecutedEventData eventData, DbDataReader result,
        CancellationToken cancellationToken = new())
    {
        if (eventData.Duration.TotalMilliseconds > QuerySlowThreshold)
        {
            LogQuery(command, eventData);
        }

        return base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
    }

    private void LogQuery(DbCommand command, CommandExecutedEventData eventData)
    {
        _logger.LogWarning($"SlowQuery:{command.CommandText}.\nTotalMilliseconds:{eventData.Duration.TotalMilliseconds}");
    }
}