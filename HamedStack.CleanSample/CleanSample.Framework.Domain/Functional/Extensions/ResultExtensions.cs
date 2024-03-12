// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

using CleanSample.Framework.Domain.Results;
using MediatR;

#pragma warning disable CS1591

namespace CleanSample.Framework.Domain.Functional.Extensions;

/*
public static class ResultExtensions
{
    public static Result<IEnumerable<object>> Aggregate(this IEnumerable<Result> results)
    {
        var resultArray = results.ToArray();
        foreach (var result in resultArray)
            if (!result.IsSuccess)
                return Result<IEnumerable<object>>.Failure(result.ErrorMessage);

        var output = resultArray.Select(r => r.Value);
        return Result<IEnumerable<object>>.Success(output!);
    }

    public static Result<TResult> Bind<TResult>(this Result result, Func<object, Result<TResult>> func)
    {
        return result.IsSuccess
            ? func(result.Value!)
            : Result<TResult>.Failure(result.ErrorMessage, result.Exception, result.MetaData);
    }

    public static Result Ensure(this Result result, Func<object?, bool> predicate, string errorMessage)
    {
        if (result.IsFailure) return result;
        return !predicate(result.Value) ? Result.Failure(errorMessage) : result;
    }

    public static Result Finally(this Result result, Action<Result> action)
    {
        action(result);
        return result;
    }

    public static Result Flatten(this Result<Result> result)
    {
        return result.IsSuccess
            ? result.Value
            : Result.Failure(result.ErrorMessage, result.Exception, result.MetaData);
    }

    public static Result IfFailure(this Result result, Action<Result> action)
    {
        if (result.IsFailure) action(result);
        return result;
    }

    public static Result<T> IfFailure<T>(this Result result, Func<T> func)
    {
        if (result.IsSuccess) return Result<T>.Success(func());
        return Result<T>.Failure(func(), result.ErrorMessage, result.Exception, result.MetaData);
    }

    public static Result IfFailure(this Result result, Action action)
    {
        if (result.IsFailure) action();
        return result;
    }

    public static Result<T> IfSuccess<T>(this Result result, Func<T> func)
    {
        if (result.IsFailure) return Result<T>.Failure(func(), result.ErrorMessage, result.Exception, result.MetaData);
        return Result<T>.Success(func());
    }
    public static Result IfSuccess(this Result result, Action<Result> action)
    {
        if (result.IsSuccess) action(result);
        return result;
    }

    public static Result IfSuccess(this Result result, Action action)
    {
        if (result.IsSuccess) action();
        return result;
    }

    public static Result Join(this IEnumerable<Result> results, string separator = ", ")
    {
        var failures = results.Where(r => !r.IsSuccess).ToList();
        if (!failures.Any()) return Result.Success();

        var combinedMessage = string.Join(separator, failures.Select(f => f.ErrorMessage));
        return Result.Failure(combinedMessage);
    }

    public static Result<(object, object)> Join(this Result result1, Result result2)
    {
        if (!result1.IsSuccess)
            return Result<(object, object)>.Failure(result1.ErrorMessage, result1.Exception, result1.MetaData);

        if (!result2.IsSuccess)
            return Result<(object, object)>.Failure(result2.ErrorMessage, result2.Exception, result2.MetaData);

        return Result<(object, object)>.Success((result1.Value!, result2.Value!));
    }

    public static Result Map<TResult>(this Result result, Func<object, TResult> mapper)
    {
        return result.IsSuccess
            ? Result.Success(mapper(result.Value!))
            : Result.Failure(result.ErrorMessage, result.Exception, result.MetaData);
    }

    public static Result MapError(this Result result,
        Func<string?, Exception?, Dictionary<string, object?>?, string?> errorMapper)
    {
        return result.IsFailure
            ? Result.Failure(errorMapper(result.ErrorMessage, result.Exception, result.MetaData), result.Exception,
                result.MetaData)
            : result;
    }

    public static TResult Match<TResult>(this Result result, Func<object, TResult> success,
        Func<string?, Exception?, Dictionary<string, object?>?, TResult> failure)
    {
        return result.IsSuccess
            ? success(result.Value!)
            : failure(result.ErrorMessage, result.Exception, result.MetaData);
    }

    public static object? OrElse(this Result result, object? defaultValue = default)
    {
        return result.IsSuccess ? result.Value : defaultValue;
    }

    public static Result<TResult> Switch<TResult>(this Result result, Func<object, Result<TResult>> success,
        Func<string?, Exception?, Dictionary<string, object?>?, Result<TResult>> failure)
    {
        return result.IsSuccess
            ? success(result.Value ?? new object()) // Ensure a non-null value for the success delegate
            : failure(result.ErrorMessage, result.Exception, result.MetaData);
    }

    public static Result Tap(this Result result, Action<object> action)
    {
        if (result.IsSuccess) action(result.Value!);
        return result;
    }

    public static Result TapError(this Result result, Action<object> action)
    {
        if (!result.IsSuccess) action(result.Value!);
        return result;
    }

    public static Result Then(this Result result, Action<object> action)
    {
        if (result.IsSuccess) action(result.Value!);
        return result;
    }

    public static Either<Unit, object> ToEither(this Result result)
    {
        return result.IsSuccess
            ? Either<Unit, object>.CreateRight(result.Value ?? new object())
            : Either<Unit, object>.CreateLeft(Unit.Value);
    }

    public static Exceptional<object> ToExceptional(this Result result)
    {
        var defaultException = new InvalidOperationException("Result status is invalid.");
        return result.IsSuccess
            ? Exceptional<object>.Success(result.Value ?? new object())
            : Exceptional<object>.Failure(result.Exception ?? defaultException);
    }

    public static Result<T> ToGenericResult<T>(this Result nonGenericResult)
    {
        return nonGenericResult.Status switch
        {
            ResultStatus.Success => Result<T>.Success((T?) nonGenericResult.Value),
            ResultStatus.Failure => Result<T>.Failure((T?) nonGenericResult.Value),
            ResultStatus.Forbidden => Result<T>.Forbidden((T?) nonGenericResult.Value),
            ResultStatus.Unauthorized => Result<T>.Unauthorized((T?) nonGenericResult.Value),
            ResultStatus.Invalid => Result<T>.Invalid((T?) nonGenericResult.Value),
            ResultStatus.NotFound => Result<T>.NotFound((T?) nonGenericResult.Value),
            ResultStatus.Conflict => Result<T>.Conflict((T?) nonGenericResult.Value),
            ResultStatus.Unsupported => Result<T>.Unsupported((T?) nonGenericResult.Value),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public static Result ToNonGenericResult(this Result genericResult)
    {
        return genericResult.Status switch
        {
            ResultStatus.Success => Result.Success(genericResult.Value),
            ResultStatus.Failure => Result.Failure(genericResult.Value),
            ResultStatus.Forbidden => Result.Forbidden(genericResult.Value),
            ResultStatus.Unauthorized => Result.Unauthorized(genericResult.Value),
            ResultStatus.Invalid => Result.Invalid(genericResult.Value),
            ResultStatus.NotFound => Result.NotFound(genericResult.Value),
            ResultStatus.Conflict => Result.Conflict(genericResult.Value),
            ResultStatus.Unsupported => Result.Unsupported(genericResult.Value),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public static Option<object> ToOption(this Result result)
    {
        return result.IsSuccess
            ? Option<object>.Some(result.Value ?? new object())
            : Option<object>.None();
    }

    public static Result Unwrap(this Result<object?> result)
    {
        return result.IsSuccess
            ? Result.Success(result.Value!)
            : Result.Failure(result.ErrorMessage, result.Exception, result.MetaData);
    }

    public static object UnwrapOr(this Result result, object defaultValue)
    {
        return result.IsSuccess ? result.Value! : defaultValue;
    }

    public static Result WithError(this Result result,
        Func<string?, Exception?, Dictionary<string, object?>?, Exception> errorWrapper)
    {
        return result.IsFailure
            ? Result.Failure(result.ErrorMessage, errorWrapper(result.ErrorMessage, result.Exception, result.MetaData),
                result.MetaData)
            : result;
    }

    public static Result WithValue(this Result result, object newValue)
    {
        return result.IsSuccess ? Result.Success(newValue) : result;
    }
}
*/