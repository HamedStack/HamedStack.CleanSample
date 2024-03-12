using CleanSample.Framework.Domain.Results;
using MediatR;

namespace CleanSample.Framework.Domain.Functional.Extensions;

/*
public static class ResultTExtensions
{
    public static Result<IEnumerable<T>> Aggregate<T>(this IEnumerable<Result<T>> results)
    {
        var resultArray = results.ToArray();
        foreach (var result in resultArray)
            if (!result.IsSuccess)
                return Result<IEnumerable<T>>.Failure(result.ErrorMessage);

        var output = resultArray.Select(r => r.Value);
        return Result<IEnumerable<T>>.Success(output!);
    }

    public static Result<TResult> Bind<T, TResult>(this Result<T> result, Func<T, Result<TResult>> func)
    {
        return result.IsSuccess
            ? func(result.Value!)
            : Result<TResult>.Failure(result.ErrorMessage, result.Exception, result.MetaData);
    }

    public static Result Combine(this IEnumerable<Result> results, string separator = ", ")
    {
        var failures = results.Where(r => r.IsFailure).ToList();
        if (!failures.Any()) return Result.Success();

        var combinedMessage = string.Join(separator, failures.Select(f => f.ErrorMessage));
        return Result.Failure(combinedMessage);
    }
    public static Result<T> Ensure<T>(this Result<T> result, Func<T?, bool> predicate, string errorMessage)
    {
        if (result.IsFailure) return result;
        return !predicate(result.Value) ? Result<T>.Failure(errorMessage) : result;
    }

    public static Result<T> Finally<T>(this Result<T> result, Action<Result<T>> action)
    {
        action(result);
        return result;
    }

    public static Result<T> Flatten<T>(this Result<Result<T>> result)
    {
        return result.IsSuccess
            ? result.Value!
            : Result<T>.Failure(result.ErrorMessage, result.Exception, result.MetaData);
    }

    public static Result<T> IfFailure<T>(this Result<T> result, Action<Result<T>> action)
    {
        if (result.IsFailure) action(result);
        return result;
    }

    public static Result<T> IfFailure<T>(this Result<T> result, Action action)
    {
        if (result.IsFailure) action();
        return result;
    }

    public static Result<T> IfSuccess<T>(this Result<T> result, Action<Result<T>> action)
    {
        if (result.IsSuccess) action(result);
        return result;
    }

    public static Result<T> IfSuccess<T>(this Result<T> result, Action action)
    {
        if (result.IsSuccess) action();
        return result;
    }

    public static Result<T> Join<T>(this IEnumerable<Result<T>> results, string separator = ", ")
    {
        var failures = results.Where(r => !r.IsSuccess).ToList();
        if (!failures.Any()) return Result<T>.Success();

        var combinedMessage = string.Join(separator, failures.Select(f => f.ErrorMessage));
        return Result<T>.Failure(combinedMessage);
    }

    public static Result<(T1, T2)> Join<T1, T2>(this Result<T1> result1, Result<T2> result2)
    {
        if (!result1.IsSuccess)
            return Result<(T1, T2)>.Failure(result1.ErrorMessage, result1.Exception, result1.MetaData);

        if (!result2.IsSuccess)
            return Result<(T1, T2)>.Failure(result2.ErrorMessage, result2.Exception, result2.MetaData);

        return Result<(T1, T2)>.Success((result1.Value!, result2.Value!));
    }

    public static Result<TResult> Map<T, TResult>(this Result<T> result, Func<T, TResult> mapper)
    {
        return result.IsSuccess
            ? Result<TResult>.Success(mapper(result.Value!))
            : Result<TResult>.Failure(result.ErrorMessage, result.Exception, result.MetaData);
    }

    public static Result<T> MapError<T>(this Result<T> result,
        Func<string?, Exception?, Dictionary<string, object?>?, string?> errorMapper)
    {
        return result.IsFailure
            ? Result<T>.Failure(errorMapper(result.ErrorMessage, result.Exception, result.MetaData), result.Exception,
                result.MetaData)
            : result;
    }

    public static TResult Match<T, TResult>(this Result<T> result, Func<T, TResult> success,
        Func<string?, Exception?, Dictionary<string, object?>?, TResult> failure)
    {
        return result.IsSuccess
            ? success(result.Value!)
            : failure(result.ErrorMessage, result.Exception, result.MetaData);
    }

    public static T? OrElse<T>(this Result<T> result, T? defaultValue = default)
    {
        return result.IsSuccess ? result.Value! : defaultValue;
    }
    public static Result<TResult> Switch<T, TResult>(this Result<T> result, Func<T, Result<TResult>> success,
        Func<string?, Exception?, Dictionary<string, object?>?, Result<TResult>> failure)
    {
        return result.IsSuccess
            ? success(result.Value!)
            : failure(result.ErrorMessage, result.Exception, result.MetaData);
    }

    public static Result<T> Tap<T>(this Result<T> result, Action<T> action)
    {
        if (result.IsSuccess) action(result.Value!);
        return result;
    }
    public static Result<T> TapError<T>(this Result<T> result, Action<T> action)
    {
        if (!result.IsSuccess) action(result.Value!);
        return result;
    }
    public static Result<T> Then<T>(this Result<T> result, Action<T> action)
    {
        if (result.IsSuccess) action(result.Value!);
        return result;
    }

    public static Either<Unit, T> ToEither<T>(this Result<T> result) where T : new()
    {
        return result.IsSuccess
            ? Either<Unit, T>.CreateRight(result.Value ?? new T())
            : Either<Unit, T>.CreateLeft(Unit.Value);
    }

    public static Exceptional<T> ToExceptional<T>(this Result<T> result) where T : new()
    {
        var defaultException = new InvalidOperationException("Result status is invalid.");
        return result.IsSuccess
            ? Exceptional<T>.Success(result.Value ?? new T())
            : Exceptional<T>.Failure(result.Exception ?? defaultException);
    }

    public static Result ToNonGenericResult<T>(this Result<T> genericResult)
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

    public static Option<T> ToOption<T>(this Result<T> result) where T : new()
    {
        return result.IsSuccess
            ? Option<T>.Some(result.Value ?? new T())
            : Option<T>.None();
    }

    public static Result<T> Unwrap<T>(this Result<T?> result)
    {
        return result.IsSuccess ? Result<T>.Success(result.Value!) : Result<T>.Failure(result.ErrorMessage, result.Exception, result.MetaData);
    }

    public static T UnwrapOr<T>(this Result<T> result, T defaultValue)
    {
        return result.IsSuccess ? result.Value! : defaultValue;
    }

    public static Result<T> WithError<T>(this Result<T> result, Func<string?, Exception?, Dictionary<string, object?>?, Exception> errorWrapper)
    {
        return result.IsFailure ? Result<T>.Failure(result.ErrorMessage, errorWrapper(result.ErrorMessage, result.Exception, result.MetaData), result.MetaData) : result;
    }

    public static Result<T> WithValue<T>(this Result<T> result, T newValue)
    {
        return result.IsSuccess ? Result<T>.Success(newValue) : result;
    }
}
*/