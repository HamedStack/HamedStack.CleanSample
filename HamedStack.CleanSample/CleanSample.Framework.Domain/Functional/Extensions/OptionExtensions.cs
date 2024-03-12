using System.Collections.Concurrent;
using System.Collections.Specialized;
using CleanSample.Framework.Domain.Results;
using MediatR;

namespace CleanSample.Framework.Domain.Functional.Extensions;

public static class OptionExtensions
{
    public static Option<List<T>> Collapse<T>(this List<Option<T>> options) where T : notnull
    {
        var resultList = new List<T>();
        foreach (var option in options)
        {
            if (option.IsNone) return new Option<List<T>>();
            resultList.Add(option.Unwrap());
        }
        return Option<List<T>>.Some(resultList);
    }

    public static Option<(T, TOther)> CombineWith<T, TOther>(this Option<T> option, Option<TOther> other) where TOther : notnull where T : notnull
    {
        if (option.IsSome && other.IsSome)
        {
            return Option<(T, TOther)>.Some((option.Unwrap(), other.Unwrap()));
        }
        return Option<(T, TOther)>.None();
    }

    public static Option<T> FirstOrNone<T>(this IEnumerable<T> source) where T : notnull
    {
        foreach (var item in source)
        {
            return Option<T>.Some(item);
        }
        return Option<T>.None();
    }

    public static Option<T> FirstOrNone<T>(this IEnumerable<T> source, Func<T, bool> predicate) where T : notnull
    {
        return source.Where(predicate).FirstOrNone();
    }

    public static Option<T> Flatten<T>(this Option<Option<T>> option) where T : notnull
    {
        return option.IsSome ? option.Unwrap() : Option<T>.None();
    }

    public static Option<T> Or<T>(this Option<T> option, Option<T> fallbackOption) where T : notnull
    {
        return option.IsSome ? option : fallbackOption;
    }

    public static async Task<T> OrElseAsync<T>(this Option<T> option, Func<Task<T>> taskFunc) where T : notnull
    {
        if (option.IsSome) return option.Unwrap();
        return await taskFunc();
    }

    public static bool Satisfies<T>(this Option<T> option, Func<T, bool> condition) where T : notnull
    {
        return option.IsSome && condition(option.Unwrap());
    }

    public static Option<TResult> SelectMany<T, TMiddle, TResult>(
        this Option<T> option,
        Func<T, Option<TMiddle>> binder,
        Func<T, TMiddle, TResult> projector) where TResult : notnull where TMiddle : notnull where T : notnull
    {
        if (option.IsNone) return Option<TResult>.None();
        var middle = binder(option.Unwrap());
        return middle.IsNone ? Option<TResult>.None() : Option<TResult>.Some(projector(option.Unwrap(), middle.Unwrap()));
    }

    public static Either<TLeft, TRight> ToEither<TLeft, TRight>(this Option<TLeft> optionLeft,
                                            Option<TRight> optionRight)
    {
        return optionLeft.Match(
            some => Either<TLeft, TRight>.CreateLeft(some!),
            () => optionRight.Match(
                some => Either<TLeft, TRight>.CreateRight(some!),
                () => throw new InvalidOperationException("Both options are None.")
            )
        );
    }

    public static Either<Option<T>, T> ToEither<T>(this Option<T> option) where T : notnull
    {
        return option.IsSome
            ? option.Unwrap()
            : (Either<Option<T>, T>)option;
    }

    public static Either<TLeft, Option<TRight>> ToEither<TLeft, TRight>(this Either<TLeft, TRight> either)
    {
        return either.IsRight
            ? Either<TLeft, Option<TRight>>.CreateRight(Option<TRight>.Some(either.Right!))
            : Either<TLeft, Option<TRight>>.CreateLeft(either.Left!);
    }

    public static Either<Unit, T> ToEitherUnit<T>(this Option<T> option)
    {
        return option.Match(
            some => Either<Unit, T>.CreateRight(some!),
            () => Either<Unit, T>.CreateLeft(Unit.Value)
        );
    }

    public static Exceptional<T> ToExceptional<T>(this Option<T> option, Exception? exceptionIfNone = null)
    {
        exceptionIfNone ??= new InvalidOperationException("None value in Option.");
        return option.Match(
            some => Exceptional<T>.Success(some!),
            () => Exceptional<T>.Failure(exceptionIfNone)
        );
    }

    public static List<T> ToList<T>(this Option<T> option) where T : notnull
    {
        return option.IsSome ? new List<T> { option.Unwrap() } : new List<T>();
    }

    public static Result ToNonGenericResult<T>(this Option<T> option, ResultStatus noneStatus = ResultStatus.Invalid)
    {
        return option.Match(
            some => Result.Success(some),
            () =>
            {
                return noneStatus switch
                {
                    ResultStatus.Forbidden => Result.Forbidden(),
                    ResultStatus.Unauthorized => Result.Unauthorized(),
                    ResultStatus.Invalid => Result.Invalid(),
                    ResultStatus.NotFound => Result.NotFound(),
                    ResultStatus.Conflict => Result.Conflict(),
                    ResultStatus.Unsupported => Result.Unsupported(),
                    _ => Result.Failure()
                };
            }
        );
    }
    public static Option<string> ToOption(this NameValueCollection collection, string key)
    {
        var value = collection[key];
        return value != null ?  Option<string>.Some(value) : Option<string>.None();
    }
    public static Option<string> ToOption(this NameValueCollection collection, int key)
    {
        var value = collection[key];
        return value != null ?  Option<string>.Some(value) : Option<string>.None();
    }
    public static Option<TValue> ToOption<TKey, TValue>(this Dictionary<TKey, TValue> @this, TKey key) where TKey : notnull where TValue : notnull
    {
        return @this.TryGetValue(key, out var value)
            ? Option<TValue>.Some(value)
            : Option<TValue>.None();
    }

    public static Option<TValue> ToOption<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> @this, TKey key) where TKey : notnull where TValue : notnull
    {
        return @this.TryGetValue(key, out var value)
            ? Option<TValue>.Some(value)
            : Option<TValue>.None();
    }

    public static Option<T> ToOption<T>(this T? value)
    {
        return value is not null ? Option<T>.Some(value) : Option<T>.None();
    }

}