// ReSharper disable UnusedMember.Global

namespace CleanSample.Framework.Domain.Results;

public class Result<T> : IResult<T>
{
    public bool IsSuccess { get; }
    public ResultStatus Status { get; }
    public T? Value { get; }
    public IDictionary<string, object> Metadata { get; } = new Dictionary<string, object>();
    public Error[]? Errors { get; }

    public Result(ResultStatus status, T? value, string error)
    {
        IsSuccess = status == ResultStatus.Success;
        Status = status;
        Value = value;
        Errors = new Error[] { new(error) };
    }

    public Result(ResultStatus status, T? value, string[] errors)
    {
        IsSuccess = status == ResultStatus.Success;
        Status = status;
        Value = value;
        Errors = errors.Select(e => new Error(e)).ToArray();
    }

    public static implicit operator Result<T>(Result value)
    {
        return Success((T?)value.Value);
    }

    public static implicit operator Result<T>(T? value)
    {
        return Success(value);
    }

    public static explicit operator T?(Result<T?> result)
    {
        if (result.IsSuccess)
        {
            return result.Value;
        }

        throw new InvalidOperationException("Cannot convert a failed Result<T> to T.");
    }

    public static explicit operator Task<T?>(Result<T?> result)
    {
        if (result.IsSuccess)
        {
            return Task.FromResult(result.Value);
        }

        throw new InvalidOperationException("Cannot convert a failed Result<T> to T.");
    }

    public static Result<T> Conflict(T? value, string error)
    {
        return new Result<T>(ResultStatus.Conflict, value, error);
    }

    public static Result<T> Failure(T? value, string error)
    {
        return new Result<T>(ResultStatus.Failure, value, error);
    }

    public static Result<T> Forbidden(T? value, string error)
    {
        return new Result<T>(ResultStatus.Forbidden, value, error);
    }

    public static Result<T> Invalid(T? value, string error)
    {
        return new Result<T>(ResultStatus.Invalid, value, error);
    }

    public static Result<T> NotFound(T? value, string error)
    {
        return new Result<T>(ResultStatus.NotFound, value, error);
    }

    public static Result<T> Success(T? value, string error)
    {
        return new Result<T>(ResultStatus.Success, value, error);
    }

    public static Result<T> Unauthorized(T? value, string error)
    {
        return new Result<T>(ResultStatus.Unauthorized, value, error);
    }

    public static Result<T> Unsupported(T? value, string error)
    {
        return new Result<T>(ResultStatus.Unsupported, value, error);
    }
    public static Result<T> Conflict(T? value, string[] errors)
    {
        return new Result<T>(ResultStatus.Conflict, value, errors);
    }

    public static Result<T> Failure(T? value, string[] errors)
    {
        return new Result<T>(ResultStatus.Failure, value, errors);
    }

    public static Result<T> Forbidden(T? value, string[] errors)
    {
        return new Result<T>(ResultStatus.Forbidden, value, errors);
    }

    public static Result<T> Invalid(T? value, string[] errors)
    {
        return new Result<T>(ResultStatus.Invalid, value, errors);
    }

    public static Result<T> NotFound(T? value, string[] errors)
    {
        return new Result<T>(ResultStatus.NotFound, value, errors);
    }

    public static Result<T> Success(T? value, string[] errors)
    {
        return new Result<T>(ResultStatus.Success, value, errors);
    }

    public static Result<T> Unauthorized(T? value, string[] errors)
    {
        return new Result<T>(ResultStatus.Unauthorized, value, errors);
    }

    public static Result<T> Unsupported(T? value, string[] errors)
    {
        return new Result<T>(ResultStatus.Unsupported, value, errors);
    }
    public Result(ResultStatus status, T? value, Error error)
    {
        IsSuccess = status == ResultStatus.Success;
        Status = status;
        Value = value;
        Errors = new Error[] { error };
    }
    public Result(ResultStatus status, T? value, Error[] errors)
    {
        IsSuccess = status == ResultStatus.Success;
        Status = status;
        Value = value;
        Errors = errors;
    }
    public static Result<T> Conflict(T? value, Error error)
    {
        return new Result<T>(ResultStatus.Conflict, value, error);
    }

    public static Result<T> Failure(T? value, Error error)
    {
        return new Result<T>(ResultStatus.Failure, value, error);
    }

    public static Result<T> Forbidden(T? value, Error error)
    {
        return new Result<T>(ResultStatus.Forbidden, value, error);
    }

    public static Result<T> Invalid(T? value, Error error)
    {
        return new Result<T>(ResultStatus.Invalid, value, error);
    }

    public static Result<T> NotFound(T? value, Error error)
    {
        return new Result<T>(ResultStatus.NotFound, value, error);
    }

    public static Result<T> Success(T? value, Error error)
    {
        return new Result<T>(ResultStatus.Success, value, error);
    }

    public static Result<T> Unauthorized(T? value, Error error)
    {
        return new Result<T>(ResultStatus.Unauthorized, value, error);
    }

    public static Result<T> Unsupported(T? value, Error error)
    {
        return new Result<T>(ResultStatus.Unsupported, value, error);
    }
    public static Result<T> Conflict(T? value, Error[] errors)
    {
        return new Result<T>(ResultStatus.Conflict, value, errors);
    }

    public static Result<T> Failure(T? value, Error[] errors)
    {
        return new Result<T>(ResultStatus.Failure, value, errors);
    }

    public static Result<T> Forbidden(T? value, Error[] errors)
    {
        return new Result<T>(ResultStatus.Forbidden, value, errors);
    }

    public static Result<T> Invalid(T? value, Error[] errors)
    {
        return new Result<T>(ResultStatus.Invalid, value, errors);
    }

    public static Result<T> NotFound(T? value, Error[] errors)
    {
        return new Result<T>(ResultStatus.NotFound, value, errors);
    }

    public static Result<T> Success(T? value, Error[] errors)
    {
        return new Result<T>(ResultStatus.Success, value, errors);
    }

    public static Result<T> Unauthorized(T? value, Error[] errors)
    {
        return new Result<T>(ResultStatus.Unauthorized, value, errors);
    }

    public static Result<T> Unsupported(T? value, Error[] errors)
    {
        return new Result<T>(ResultStatus.Unsupported, value, errors);
    }
    public Result(ResultStatus status, T? value)
    {
        IsSuccess = status == ResultStatus.Success;
        Status = status;
        Value = value;
        Errors = null;
    }

    public static Result<T> Conflict(T? value)
    {
        return new Result<T>(ResultStatus.Conflict, value);
    }

    public static Result<T> Failure(T? value)
    {
        return new Result<T>(ResultStatus.Failure, value);
    }

    public static Result<T> Forbidden(T? value)
    {
        return new Result<T>(ResultStatus.Forbidden, value);
    }

    public static Result<T> Invalid(T? value)
    {
        return new Result<T>(ResultStatus.Invalid, value);
    }

    public static Result<T> NotFound(T? value)
    {
        return new Result<T>(ResultStatus.NotFound, value);
    }

    public static Result<T> Success(T? value)
    {
        return new Result<T>(ResultStatus.Success, value);
    }

    public static Result<T> Unauthorized(T? value)
    {
        return new Result<T>(ResultStatus.Unauthorized, value);
    }

    public static Result<T> Unsupported(T? value)
    {
        return new Result<T>(ResultStatus.Unsupported, value);
    }

    object? IResult.Value => Value;


    public Result<TResult> Map<TResult>(Func<T?, TResult> mapper)
    {
        var result = mapper(Value);
        return IsSuccess
            ? Result<TResult>.Success(result)
            : Result<TResult>.Failure(result, Errors!);
    }

    public TResult Match<TResult>(
        Func<T?, TResult> onSuccess,
        Func<Error[], TResult> onFailure)
    {
        return IsSuccess ? onSuccess(Value) : onFailure(Errors!);
    }

    public Result<T> Recover(Func<T?, Result<T>> recovery)
    {
        return IsSuccess ? this : recovery(Value);
    }

    public Result<TResult> Select<TResult>(Func<T?, TResult> selector)
    {
        var result = selector(Value);
        return IsSuccess
            ? Result<TResult>.Success(result)
            : Result<TResult>.Failure(result, Errors!);
    }

    public Result<TResult> SelectMany<TResult>(Func<T?, Result<TResult>> selector)
    {
        var result = selector(Value);
        return IsSuccess ? result : Result<TResult>.Failure(result.Value, Errors!);
    }

    public T? UnwrapOrDefault()
    {
        return IsSuccess ? Value : default;
    }

    public T? UnwrapOrDefault(T? defaultValue)
    {
        return IsSuccess ? Value : defaultValue;
    }

    public T? UnwrapOrThrow()
    {
        if (IsSuccess) return Value;

        throw new InvalidOperationException("Cannot unwrap a failed result.");
    }

    public T? UnwrapOrThrow(Func<Exception> exceptionProvider)
    {
        if (IsSuccess)
        {
            return Value;
        }

        throw exceptionProvider();
    }

    public Result<T> Where(Func<T?, bool> predicate)
    {
        if (!IsSuccess || !predicate(Value))
        {
            return Failure(Value, Errors!);
        }

        return this;
    }
}
