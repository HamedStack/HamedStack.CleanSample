using CleanSample.Framework.Domain.Results;

namespace CleanSample.Framework.Domain.Functional.Extensions;

public static class ExceptionalExtensions
{
    public static Exceptional<TResult> Bind<T, TResult>(
        this Exceptional<T> exceptional,
        Func<T, Exceptional<TResult>> func)
    {
        return exceptional.HasValue ? func(exceptional.Value!) : Exceptional<TResult>.Failure(exceptional.Exception!);
    }

    public static Exceptional<TResult> Bind<T, TResult>(
        this Exceptional<T> exceptional,
        Option<TResult> optionResult) where TResult : notnull
    {
        if (!exceptional.HasValue) return Exceptional<TResult>.Failure(exceptional.Exception!);

        return optionResult.IsSome
            ? Exceptional<TResult>.Success(optionResult.Unwrap())
            : Exceptional<TResult>.Failure(new InvalidOperationException("The provided Option is None"));
    }

    public static Exceptional<TResult> Bind<T, TResult>(
        this Exceptional<T> exceptional,
        Func<T, Option<TResult>> func) where TResult : notnull
    {
        if (!exceptional.HasValue) return Exceptional<TResult>.Failure(exceptional.Exception!);

        var optionResult = func(exceptional.Value!);

        return optionResult.IsSome
            ? Exceptional<TResult>.Success(optionResult.Unwrap())
            : Exceptional<TResult>.Failure(new InvalidOperationException("The function returned None"));
    }

    public static Exceptional<TRight> Flatten<TRight>(this Exceptional<Exceptional<TRight>> exceptional)
    {
        return exceptional.HasException ? Exceptional<TRight>.Failure(exceptional.Exception!) : exceptional.Value;
    }

    public static void ForEach<T>(
        this Exceptional<T> exceptional,
        Action<T> action)
    {
        if (exceptional.HasValue)
        {
            action(exceptional.Value!);
        }
    }

    public static Exceptional<TResult> Map<T, TResult>(
        this Exceptional<T> exceptional,
        Func<T, TResult> func)
    {
        return exceptional.HasValue
            ? Exceptional<TResult>.Success(func(exceptional.Value!)!)
            : Exceptional<TResult>.Failure(exceptional.Exception!);
    }

    public static Exceptional<T> MapException<T>(this Exceptional<T> exceptional, Func<Exception, Exception> func)
    {
        return exceptional.HasException ? Exceptional<T>.Failure(func(exceptional.Exception!)) : exceptional;
    }

    public static TResult Match<T, TResult>(
        this Exceptional<T> exceptional,
        Func<T, TResult> onSuccess,
        Func<Exception, TResult> onError)
    {
        return exceptional.HasValue ? onSuccess(exceptional.Value!) : onError(exceptional.Exception!);
    }

    public static Exceptional<T> Recover<T>(this Exceptional<T> exceptional, Func<Exception, T> recoveryFunc)
    {
        if (exceptional.HasValue)
            return exceptional;
        if (exceptional is { HasException: true, Exception: not null })
            return Exceptional<T>.Success(recoveryFunc(exceptional.Exception)!);
        throw new InvalidOperationException("The exceptional neither has a value nor an exception.");
    }

    public static Exceptional<TResult> Select<T, TResult>(this Exceptional<T> exp, Func<T, TResult> f)
        => exp.Map(f);

    public static Either<T, Exception> ToEither<T>(this Exceptional<T> exceptional)
    {
        return exceptional.Match(
            success => Either<T, Exception>.CreateLeft(success!),
            Either<T, Exception>.CreateRight
        );
    }

    /*
    public static Result<T> ToGenericResult<T>(this Exceptional<T> exceptional)
    {
        return exceptional.Match(
            Result<T>.Success,
            failure => Result<T>.Failure(exception: failure)
        );
    }

    public static Result ToNonGenericResult<T>(this Exceptional<T> exceptional)
    {
        return exceptional.Match(
            success => Result.Success(success),
            failure => Result.Failure(exception: failure)
        );
    }
    */

    public static Option<T> ToOption<T>(this Exceptional<T> exceptional)
    {
        return exceptional.Match(
            success => Option<T>.Some(success!),
            _ => Option<T>.None()
        );
    }
}