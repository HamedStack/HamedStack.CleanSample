using System.Collections.Concurrent;
using System.Collections.Specialized;
using MediatR;

namespace CleanSample.Framework.Domain.Functional.Extensions;

public static class MaybeExtensions
{
    public static Maybe<List<T>> Collapse<T>(this List<Maybe<T>> maybes) where T : notnull
    {
        var resultList = new List<T>();
        foreach (var maybe in maybes)
        {
            if (maybe.IsNothing) return new Maybe<List<T>>();
            resultList.Add(maybe.Unwrap());
        }
        return Maybe<List<T>>.Just(resultList);
    }

    public static Maybe<(T, TOther)> CombineWith<T, TOther>(this Maybe<T> maybe, Maybe<TOther> other) where TOther : notnull where T : notnull
    {
        if (maybe.IsJust && other.IsJust)
        {
            return Maybe<(T, TOther)>.Just((maybe.Unwrap(), other.Unwrap()));
        }
        return Maybe<(T, TOther)>.Nothing();
    }

    public static Maybe<T> FirstOrNothing<T>(this IEnumerable<T> source) where T : notnull
    {
        foreach (var item in source)
        {
            return Maybe<T>.Just(item);
        }
        return Maybe<T>.Nothing();
    }

    public static Maybe<T> FirstOrNothing<T>(this IEnumerable<T> source, Func<T, bool> predicate) where T : notnull
    {
        return source.Where(predicate).FirstOrNothing();
    }

    public static Maybe<T> Flatten<T>(this Maybe<Maybe<T>> maybe) where T : notnull
    {
        return maybe.IsJust ? maybe.Unwrap() : Maybe<T>.Nothing();
    }

    public static Maybe<T> Or<T>(this Maybe<T> maybe, Maybe<T> fallbackMaybe) where T : notnull
    {
        return maybe.IsJust ? maybe : fallbackMaybe;
    }

    public static async Task<T> OrElseAsync<T>(this Maybe<T> maybe, Func<Task<T>> taskFunc) where T : notnull
    {
        if (maybe.IsJust) return maybe.Unwrap();
        return await taskFunc();
    }

    public static bool Satisfies<T>(this Maybe<T> maybe, Func<T, bool> condition) where T : notnull
    {
        return maybe.IsJust && condition(maybe.Unwrap());
    }

    public static Maybe<TResult> SelectMany<T, TMiddle, TResult>(
        this Maybe<T> maybe,
        Func<T, Maybe<TMiddle>> binder,
        Func<T, TMiddle, TResult> projector) where TResult : notnull where TMiddle : notnull where T : notnull
    {
        if (maybe.IsNothing) return Maybe<TResult>.Nothing();
        var middle = binder(maybe.Unwrap());
        return middle.IsNothing ? Maybe<TResult>.Nothing() : Maybe<TResult>.Just(projector(maybe.Unwrap(), middle.Unwrap()));
    }

    public static Either<TLeft, TRight> ToEither<TLeft, TRight>(this Maybe<TLeft> optionLeft,
                                            Maybe<TRight> optionRight)
    {
        return optionLeft.Match(
            some => Either<TLeft, TRight>.CreateLeft(some!),
            () => optionRight.Match(
                some => Either<TLeft, TRight>.CreateRight(some!),
                () => throw new InvalidOperationException("Both maybes are None.")
            )
        );
    }

    public static Either<Maybe<T>, T> ToEither<T>(this Maybe<T> maybe) where T : notnull
    {
        return maybe.IsJust
            ? maybe.Unwrap()
            : (Either<Maybe<T>, T>)maybe;
    }

    public static Either<TLeft, Maybe<TRight>> ToEither<TLeft, TRight>(this Either<TLeft, TRight> either)
    {
        return either.IsRight
            ? Either<TLeft, Maybe<TRight>>.CreateRight(Maybe<TRight>.Just(either.Right!))
            : Either<TLeft, Maybe<TRight>>.CreateLeft(either.Left!);
    }

    public static Either<Unit, T> ToEitherUnit<T>(this Maybe<T> maybe)
    {
        return maybe.Match(
            some => Either<Unit, T>.CreateRight(some!),
            () => Either<Unit, T>.CreateLeft(Unit.Value)
        );
    }

    public static Exceptional<T> ToExceptional<T>(this Maybe<T> maybe, Exception? exceptionIfNone = null)
    {
        exceptionIfNone ??= new InvalidOperationException("None value in Maybe.");
        return maybe.Match(
            some => Exceptional<T>.Success(some!),
            () => Exceptional<T>.Failure(exceptionIfNone)
        );
    }

    public static List<T> ToList<T>(this Maybe<T> maybe) where T : notnull
    {
        return maybe.IsJust ? new List<T> { maybe.Unwrap() } : new List<T>();
    }

    public static Maybe<string> ToMaybe(this NameValueCollection collection, string key)
    {
        var value = collection[key];
        return value != null ?  Maybe<string>.Just(value) : Maybe<string>.Nothing();
    }
    public static Maybe<string> ToMaybe(this NameValueCollection collection, int key)
    {
        var value = collection[key];
        return value != null ?  Maybe<string>.Just(value) : Maybe<string>.Nothing();
    }
    public static Maybe<TValue> ToMaybe<TKey, TValue>(this Dictionary<TKey, TValue> @this, TKey key) where TKey : notnull where TValue : notnull
    {
        return @this.TryGetValue(key, out var value)
            ? Maybe<TValue>.Just(value)
            : Maybe<TValue>.Nothing();
    }

    public static Maybe<TValue> ToMaybe<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> @this, TKey key) where TKey : notnull where TValue : notnull
    {
        return @this.TryGetValue(key, out var value)
            ? Maybe<TValue>.Just(value)
            : Maybe<TValue>.Nothing();
    }

    public static Maybe<T> ToMaybe<T>(this T? value)
    {
        return value is not null ? Maybe<T>.Just(value) : Maybe<T>.Nothing();
    }

}