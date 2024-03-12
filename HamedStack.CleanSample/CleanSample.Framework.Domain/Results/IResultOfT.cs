// ReSharper disable UnusedMember.Global
namespace CleanSample.Framework.Domain.Results;

public interface IResult<out T> : IResult
{
    new T? Value { get; }
}