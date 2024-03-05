// ReSharper disable UnusedMember.Global
namespace CleanSample.Framework.Application.Results;

public interface IResult<out T> : IResult
{
    new T? Value { get; }
}