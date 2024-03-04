// ReSharper disable UnusedMember.Global
namespace CleanSample.SharedKernel.Application.Results;

public interface IResult<out T> : IResult
{
    new T? Value { get; }
}