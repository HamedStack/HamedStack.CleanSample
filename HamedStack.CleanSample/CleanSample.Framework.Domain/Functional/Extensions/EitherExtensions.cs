﻿namespace CleanSample.Framework.Domain.Functional.Extensions;

public static class EitherExtensions
{
    public static Either<TLeft, TResult> Bind<TLeft, TRight, TResult>(
        this Either<TLeft, TRight> either,
        Func<TRight, Either<TLeft, TResult>> binder)
    {
        return either.IsRight ? binder(either.Right!) : Either<TLeft, TResult>.CreateLeft(either.Left!);
    }

    public static void ForEachLeft<TLeft, TRight>(
        this Either<TLeft, TRight> either,
        Action<TLeft> action)
    {
        if (either.IsLeft)
        {
            action(either.Left!);
        }
    }

    public static void ForEachRight<TLeft, TRight>(
        this Either<TLeft, TRight> either,
        Action<TRight> action)
    {
        if (either.IsRight)
        {
            action(either.Right!);
        }
    }

    public static TLeft GetLeftOr<TLeft, TRight>(this Either<TLeft, TRight> either, TLeft defaultValue)
    {
        return either.IsLeft ? either.Left! : defaultValue;
    }

    public static TRight GetRightOr<TLeft, TRight>(this Either<TLeft, TRight> either, TRight defaultValue)
    {
        return either.IsRight ? either.Right! : defaultValue;
    }

    public static Maybe<TLeft> LeftProjection<TLeft, TRight>(this Either<TLeft, TRight> either)
    {
        return either.Left is not null ? Maybe<TLeft>.Just(either.Left) : Maybe<TLeft>.Nothing();
    }

    public static Either<TResult, TRight> MapLeft<TLeft, TRight, TResult>(
        this Either<TLeft, TRight> either,
        Func<TLeft, TResult> func)
    {
        return either.IsLeft
            ? Either<TResult, TRight>.CreateLeft(func(either.Left!)!)
            : Either<TResult, TRight>.CreateRight(either.Right!);
    }

    public static Either<TLeft, TResult> MapRight<TLeft, TRight, TResult>(
        this Either<TLeft, TRight> either,
        Func<TRight, TResult> func)
    {
        return either.IsRight
            ? Either<TLeft, TResult>.CreateRight(func(either.Right!)!)
            : Either<TLeft, TResult>.CreateLeft(either.Left!);
    }

    public static Maybe<TRight> RightProjection<TLeft, TRight>(this Either<TLeft, TRight> either)
    {
        return either.Right is not null ? Maybe<TRight>.Just(either.Right) : Maybe<TRight>.Nothing();
    }

    public static Either<TRight, TLeft> Swap<TLeft, TRight>(this Either<TLeft, TRight> either)
    {
        return either.IsLeft
            ? Either<TRight, TLeft>.CreateRight(either.Left!)
            : Either<TRight, TLeft>.CreateLeft(either.Right!);
    }

    public static Exceptional<TRight> ToExceptional<TLeft, TRight>(this Either<TLeft, TRight> either)
    {
        return either.Match(
            _ => Exceptional<TRight>.Failure(new InvalidOperationException("Left value in Either.")),
            right => Exceptional<TRight>.Success(right!)
        );
    }

    public static Exceptional<TRight> ToExceptional<TLeft, TRight>(this Either<TLeft, TRight> either,
        Exception fallbackException)
    {
        return either.Match(
            _ => Exceptional<TRight>.Failure(fallbackException),
            right => Exceptional<TRight>.Success(right!)
        );
    }

    public static Maybe<TRight> ToMaybe<TLeft, TRight>(this Either<TLeft, TRight> either)
    {
        return either.Match(
            _ => Maybe<TRight>.Nothing(),
            right => Maybe<TRight>.Just(right!)
        );
    }
}