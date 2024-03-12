// ReSharper disable UnusedMember.Global
namespace CleanSample.Domain.Results;

public interface IResult<out T> : IResult
{
    new T? Value { get; }
}