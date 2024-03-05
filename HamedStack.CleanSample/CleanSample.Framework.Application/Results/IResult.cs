namespace CleanSample.Framework.Application.Results;

public interface IResult
{
    bool IsSuccess { get; }
    ResultStatus Status { get; }
    Error? Error { get; }
    object? Value { get; }
    IDictionary<string, object> Metadata { get; }

}