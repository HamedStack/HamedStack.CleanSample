// ReSharper disable UnusedMember.Global

namespace CleanSample.Framework.Domain.Results;

public class Result : IResult
{
    public bool IsSuccess { get; }
    public ResultStatus Status { get; }
    public object? Value { get; }
    public IDictionary<string, object> Metadata { get; } = new Dictionary<string, object>();
    public Error[]? Errors { get; }

    public Result(ResultStatus status)
    {
        IsSuccess = status == ResultStatus.Success;
        Status = status;
        Value = null;
        Errors = null;
    }

    public static Result Conflict()
    {
        return new Result(ResultStatus.Conflict);
    }
    public static Result Failure()
    {
        return new Result(ResultStatus.Failure);
    }
    public static Result Forbidden()
    {
        return new Result(ResultStatus.Forbidden);
    }
    public static Result Invalid()
    {
        return new Result(ResultStatus.Invalid);
    }
    public static Result NotFound()
    {
        return new Result(ResultStatus.NotFound);
    }
    public static Result Success()
    {
        return new Result(ResultStatus.Success);
    }
    public static Result Unauthorized()
    {
        return new Result(ResultStatus.Unauthorized);
    }
    public static Result Unsupported()
    {
        return new Result(ResultStatus.Unsupported);
    }
    public Result(ResultStatus status, string error)
    {
        IsSuccess = status == ResultStatus.Success;
        Status = status;
        Value = null;
        Errors = new Error[] { new(error) };
    }

    public Result(ResultStatus status, string[] errors)
    {
        IsSuccess = status == ResultStatus.Success;
        Status = status;
        Value = null;
        Errors = errors.Select(e => new Error(e)).ToArray();
    }

    public static Result Conflict(string error)
    {
        return new Result(ResultStatus.Conflict, error);
    }
    public static Result Failure(string error)
    {
        return new Result(ResultStatus.Failure, error);
    }
    public static Result Forbidden(string error)
    {
        return new Result(ResultStatus.Forbidden, error);
    }
    public static Result Invalid(string error)
    {
        return new Result(ResultStatus.Invalid, error);
    }
    public static Result NotFound(string error)
    {
        return new Result(ResultStatus.NotFound, error);
    }
    public static Result Success(string error)
    {
        return new Result(ResultStatus.Success, error);
    }
    public static Result Unauthorized(string error)
    {
        return new Result(ResultStatus.Unauthorized, error);
    }
    public static Result Unsupported(string error)
    {
        return new Result(ResultStatus.Unsupported, error);
    }

    public static Result Conflict(string[] errors)
    {
        return new Result(ResultStatus.Conflict, errors);
    }
    public static Result Failure(string[] errors)
    {
        return new Result(ResultStatus.Failure, errors);
    }
    public static Result Forbidden(string[] errors)
    {
        return new Result(ResultStatus.Forbidden, errors);
    }
    public static Result Invalid(string[] errors)
    {
        return new Result(ResultStatus.Invalid, errors);
    }
    public static Result NotFound(string[] errors)
    {
        return new Result(ResultStatus.NotFound, errors);
    }
    public static Result Success(string[] errors)
    {
        return new Result(ResultStatus.Success, errors);
    }
    public static Result Unauthorized(string[] errors)
    {
        return new Result(ResultStatus.Unauthorized, errors);
    }
    public static Result Unsupported(string[] errors)
    {
        return new Result(ResultStatus.Unsupported, errors);
    }
    public Result(ResultStatus status, Error error)
    {
        IsSuccess = status == ResultStatus.Success;
        Status = status;
        Value = null;
        Errors = new Error[] { error };
    }
    public Result(ResultStatus status, Error[] errors)
    {
        IsSuccess = status == ResultStatus.Success;
        Status = status;
        Value = null;
        Errors = errors;
    }
    public static Result Conflict(Error error)
    {
        return new Result(ResultStatus.Conflict, error);
    }
    public static Result Failure(Error error)
    {
        return new Result(ResultStatus.Failure, error);
    }
    public static Result Forbidden(Error error)
    {
        return new Result(ResultStatus.Forbidden, error);
    }
    public static Result Invalid(Error error)
    {
        return new Result(ResultStatus.Invalid, error);
    }
    public static Result NotFound(Error error)
    {
        return new Result(ResultStatus.NotFound, error);
    }
    public static Result Success(Error error)
    {
        return new Result(ResultStatus.Success, error);
    }
    public static Result Unauthorized(Error error)
    {
        return new Result(ResultStatus.Unauthorized, error);
    }
    public static Result Unsupported(Error error)
    {
        return new Result(ResultStatus.Unsupported, error);
    }

    public static Result Conflict(Error[] errors)
    {
        return new Result(ResultStatus.Conflict, errors);
    }
    public static Result Failure(Error[] errors)
    {
        return new Result(ResultStatus.Failure, errors);
    }
    public static Result Forbidden(Error[] errors)
    {
        return new Result(ResultStatus.Forbidden, errors);
    }
    public static Result Invalid(Error[] errors)
    {
        return new Result(ResultStatus.Invalid, errors);
    }
    public static Result NotFound(Error[] errors)
    {
        return new Result(ResultStatus.NotFound, errors);
    }
    public static Result Success(Error[] errors)
    {
        return new Result(ResultStatus.Success, errors);
    }
    public static Result Unauthorized(Error[] errors)
    {
        return new Result(ResultStatus.Unauthorized, errors);
    }
    public static Result Unsupported(Error[] errors)
    {
        return new Result(ResultStatus.Unsupported, errors);
    }

    public Result(ResultStatus status, object? value, string error)
    {
        IsSuccess = status == ResultStatus.Success;
        Status = status;
        Value = value;
        Errors = new Error[] { new(error) };
    }
    public Result(ResultStatus status, object? value, string[] errors)
    {
        IsSuccess = status == ResultStatus.Success;
        Status = status;
        Value = value;
        Errors = errors.Select(e => new Error(e)).ToArray();
    }
    public static Result Conflict(object? value, string error)
    {
        return new Result(ResultStatus.Conflict, value, error);
    }
    public static Result Failure(object? value, string error)
    {
        return new Result(ResultStatus.Failure, value, error);
    }
    public static Result Forbidden(object? value, string error)
    {
        return new Result(ResultStatus.Forbidden, value, error);
    }
    public static Result Invalid(object? value, string error)
    {
        return new Result(ResultStatus.Invalid, value, error);
    }
    public static Result NotFound(object? value, string error)
    {
        return new Result(ResultStatus.NotFound, value, error);
    }
    public static Result Success(object? value, string error)
    {
        return new Result(ResultStatus.Success, value, error);
    }
    public static Result Unauthorized(object? value, string error)
    {
        return new Result(ResultStatus.Unauthorized, value, error);
    }
    public static Result Unsupported(object? value, string error)
    {
        return new Result(ResultStatus.Unsupported, value, error);
    }

    public static Result Conflict(object? value, string[] errors)
    {
        return new Result(ResultStatus.Conflict, value, errors);
    }
    public static Result Failure(object? value, string[] errors)
    {
        return new Result(ResultStatus.Failure, value, errors);
    }
    public static Result Forbidden(object? value, string[] errors)
    {
        return new Result(ResultStatus.Forbidden, value, errors);
    }
    public static Result Invalid(object? value, string[] errors)
    {
        return new Result(ResultStatus.Invalid, value, errors);
    }
    public static Result NotFound(object? value, string[] errors)
    {
        return new Result(ResultStatus.NotFound, value, errors);
    }
    public static Result Success(object? value, string[] errors)
    {
        return new Result(ResultStatus.Success, value, errors);
    }
    public static Result Unauthorized(object? value, string[] errors)
    {
        return new Result(ResultStatus.Unauthorized, value, errors);
    }
    public static Result Unsupported(object? value, string[] errors)
    {
        return new Result(ResultStatus.Unsupported, value, errors);
    }

    public Result(ResultStatus status, object? value, Error error)
    {
        IsSuccess = status == ResultStatus.Success;
        Status = status;
        Value = value;
        Errors = new Error[] { error };
    }

    public Result(ResultStatus status, object? value, Error[] errors)
    {
        IsSuccess = status == ResultStatus.Success;
        Status = status;
        Value = value;
        Errors = errors;
    }

    public static Result Conflict(object? value, Error error)
    {
        return new Result(ResultStatus.Conflict, value, error);
    }
    public static Result Failure(object? value, Error error)
    {
        return new Result(ResultStatus.Failure, value, error);
    }
    public static Result Forbidden(object? value, Error error)
    {
        return new Result(ResultStatus.Forbidden, value, error);
    }
    public static Result Invalid(object? value, Error error)
    {
        return new Result(ResultStatus.Invalid, value, error);
    }
    public static Result NotFound(object? value, Error error)
    {
        return new Result(ResultStatus.NotFound, value, error);
    }
    public static Result Success(object? value, Error error)
    {
        return new Result(ResultStatus.Success, value, error);
    }
    public static Result Unauthorized(object? value, Error error)
    {
        return new Result(ResultStatus.Unauthorized, value, error);
    }
    public static Result Unsupported(object? value, Error error)
    {
        return new Result(ResultStatus.Unsupported, value, error);
    }
    public static Result Conflict(object? value, Error[] errors)
    {
        return new Result(ResultStatus.Conflict, value, errors);
    }
    public static Result Failure(object? value, Error[] errors)
    {
        return new Result(ResultStatus.Failure, value, errors);
    }
    public static Result Forbidden(object? value, Error[] errors)
    {
        return new Result(ResultStatus.Forbidden, value, errors);
    }
    public static Result Invalid(object? value, Error[] errors)
    {
        return new Result(ResultStatus.Invalid, value, errors);
    }
    public static Result NotFound(object? value, Error[] errors)
    {
        return new Result(ResultStatus.NotFound, value, errors);
    }
    public static Result Success(object? value, Error[] errors)
    {
        return new Result(ResultStatus.Success, value, errors);
    }
    public static Result Unauthorized(object? value, Error[] errors)
    {
        return new Result(ResultStatus.Unauthorized, value, errors);
    }
    public static Result Unsupported(object? value, Error[] errors)
    {
        return new Result(ResultStatus.Unsupported, value, errors);
    }
    public Result(ResultStatus status, object? value)
    {
        IsSuccess = status == ResultStatus.Success;
        Status = status;
        Value = value;
        Errors = null;
    }

    public static Result Conflict(object? value)
    {
        return new Result(ResultStatus.Conflict, value);
    }
    public static Result Failure(object? value)
    {
        return new Result(ResultStatus.Failure, value);
    }
    public static Result Forbidden(object? value)
    {
        return new Result(ResultStatus.Forbidden, value);
    }
    public static Result Invalid(object? value)
    {
        return new Result(ResultStatus.Invalid, value);
    }
    public static Result NotFound(object? value)
    {
        return new Result(ResultStatus.NotFound, value);
    }
    public static Result Success(object? value)
    {
        return new Result(ResultStatus.Success, value);
    }
    public static Result Unauthorized(object? value)
    {
        return new Result(ResultStatus.Unauthorized, value);
    }
    public static Result Unsupported(object? value)
    {
        return new Result(ResultStatus.Unsupported, value);
    }

    public Result Map(Func<object?, object> mapper)
    {
        var result = mapper(Value);
        return IsSuccess
            ? Success(result)
            : Failure(result, Errors!);
    }
    public TResult Match<TResult>(
        Func<object?, TResult> onSuccess,
        Func<Error[], TResult> onFailure)
    {
        return IsSuccess ? onSuccess(Value) : onFailure(Errors!);
    }

    public Result Recover(Func<object?, Result> recovery)
    {
        return IsSuccess ? this : recovery(Value);
    }

    public Result Select(Func<object?, object> selector)
    {
        var result = selector(Value);
        return IsSuccess
            ? Success(result)
            : Failure(result, Errors!);
    }

    public Result SelectMany(Func<object?, Result> selector)
    {
        var result = selector(Value);
        if (IsSuccess)
        {
            return result;
        }
        return result.Value != null ? Failure(result.Value, Errors!) : Failure(Errors!);
    }
    public object? UnwrapOrDefault()
    {
        return IsSuccess ? Value : default;
    }

    public object? UnwrapOrDefault(object? defaultValue)
    {
        return IsSuccess ? Value : defaultValue;
    }
    public object? UnwrapOrThrow()
    {
        if (IsSuccess) return Value;

        throw new InvalidOperationException("Cannot unwrap a failed result.");
    }
    public object? UnwrapOrThrow(Func<Exception> exceptionProvider)
    {
        if (IsSuccess)
        {
            return Value;
        }
        throw exceptionProvider();
    }
    public Result Where(Func<object?, bool> predicate)
    {
        if (!IsSuccess || !predicate(Value))
        {
            return Failure(Value!, Errors!);
        }
        return this;
    }
}
