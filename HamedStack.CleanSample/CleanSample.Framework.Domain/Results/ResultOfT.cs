using System.Text.Json.Serialization;

namespace CleanSample.Framework.Domain.Results;

public class Result<T> : Result
{
    protected Result() { }
    public Result(T? value)
    {
        Value = value;
    }

    public T? Value { get; set; }

    [JsonIgnore]
    public Type ValueType => typeof(T);

    public static Result<T> Success(T? value)
    {
        return new Result<T> { IsSuccess = true, Value = value, Status = ResultStatus.Success };
    }
    public static Result<T> Success(T? value, string successMessage)
    {
        return new Result<T> { IsSuccess = true, Value = value, SuccessMessage = successMessage, Status = ResultStatus.Success };
    }
    public static Result<T> Error(T? value, params string[] errorMessages)
    {
        return new Result<T> { IsSuccess = false, Value = value, ErrorMessages = errorMessages, Status = ResultStatus.Error };
    }

    public static Result<T> Forbidden(T? value, params string[] errorMessages)
    {
        return new Result<T> { IsSuccess = false, Value = value, ErrorMessages = errorMessages, Status = ResultStatus.Forbidden };
    }

    public static Result<T> Unauthorized(T? value, params string[] errorMessages)
    {
        return new Result<T> { IsSuccess = false, Value = value, ErrorMessages = errorMessages, Status = ResultStatus.Unauthorized };
    }

    public static Result<T> Invalid(T? value, params string[] errorMessages)
    {
        return new Result<T> { IsSuccess = false, Value = value, ErrorMessages = errorMessages, Status = ResultStatus.Invalid };
    }

    public static Result<T> NotFound(T? value, params string[] errorMessages)
    {
        return new Result<T> { IsSuccess = false, Value = value, ErrorMessages = errorMessages, Status = ResultStatus.NotFound };
    }

    public static Result<T> Conflict(T? value, params string[] errorMessages)
    {
        return new Result<T> { IsSuccess = false, Value = value, ErrorMessages = errorMessages, Status = ResultStatus.Conflict };
    }

    public static Result<T> Unavailable(T? value, params string[] errorMessages)
    {
        return new Result<T> { IsSuccess = false, Value = value, ErrorMessages = errorMessages, Status = ResultStatus.Unavailable };
    }

    public static Result<T> Unsupported(T? value, params string[] errorMessages)
    {
        return new Result<T> { IsSuccess = false, Value = value, ErrorMessages = errorMessages, Status = ResultStatus.Unsupported };
    }

    public static implicit operator T?(Result<T> result) => result.Value;
    public static implicit operator Result<T>(T value) => new(value);

}