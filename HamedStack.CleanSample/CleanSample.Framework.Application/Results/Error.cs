namespace CleanSample.Framework.Application.Results;

public class Error(string message, ErrorSeverity severity = ErrorSeverity.Error, Exception? exception = null)
{
    public string Message { get; } = message;
    public Exception? Exception { get; } = exception;
    public ErrorSeverity Severity { get; } = severity;
}