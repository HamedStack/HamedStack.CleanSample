// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

using System.Diagnostics.CodeAnalysis;

#pragma warning disable CS1591

namespace CleanSample.Framework.Domain.Functional;

public readonly struct Either<TLeft, TRight> : IEquatable<Either<TLeft, TRight>>
{
    private Either(TLeft? left, TRight? right)
    {
        if (left is not null && right is not null)
            throw new ArgumentException("Either must have either a Left or a Right value, but not both.");

        if (left is null && right is null)
            throw new ArgumentException("Either must have either a Left or a Right value; but both are null.");

        Left = left;
        Right = right;
    }

    private Either([DisallowNull] TLeft left)
    {
        Left = left ?? throw new ArgumentNullException(nameof(left));
    }

    private Either([DisallowNull] TRight right)
    {
        Right = right ?? throw new ArgumentNullException(nameof(right));
    }

    public bool IsLeft => Left is not null;
    public bool IsRight => Right is not null;
    public TLeft? Left { get; }
    public TRight? Right { get; }
    public static Either<TLeft, TRight> Create(TLeft? left, TRight? right)
    {
        return new Either<TLeft, TRight>(left, right);
    }

    public static Either<TLeft, TRight> CreateLeft([DisallowNull] TLeft left)
    {
        if (left == null) throw new ArgumentNullException(nameof(left));
        return new Either<TLeft, TRight>(left);
    }

    public static Either<TLeft, TRight> CreateRight([DisallowNull] TRight right)
    {
        if (right == null) throw new ArgumentNullException(nameof(right));
        return new Either<TLeft, TRight>(right);
    }

    public static explicit operator TLeft(Either<TLeft, TRight> either)
    {
        if (either.IsLeft)
            return either.Left!;
        throw new InvalidCastException("Cannot cast to TLeft when Either is a Right.");
    }

    public static explicit operator TRight(Either<TLeft, TRight> either)
    {
        if (either.IsRight)
            return either.Right!;
        throw new InvalidCastException("Cannot cast to TRight when Either is a Left.");
    }

    public static implicit operator Either<TLeft, TRight>(TLeft left)
    {
        return new Either<TLeft, TRight>(left!);
    }

    public static implicit operator Either<TLeft, TRight>(TRight right)
    {
        return new Either<TLeft, TRight>(right!);
    }
    public static bool IsEither(object obj)
    {
        return obj is Either<TLeft, TRight>;
    }

    public static bool operator !=(Either<TLeft, TRight> left, Either<TLeft, TRight> right)
    {
        return !(left == right);
    }

    public static bool operator ==(Either<TLeft, TRight> left, Either<TLeft, TRight> right)
    {
        return left.Equals(right);
    }
    public Either<T, TRight> BindLeft<T>(Func<TLeft, Either<T, TRight>> func)
    {
        return IsLeft ? func(Left!) : new Either<T, TRight>(default, Right);
    }

    public Either<TLeft, T> BindRight<T>(Func<TRight, Either<TLeft, T>> func)
    {
        return IsRight ? func(Right!) : new Either<TLeft, T>(Left, default);
    }

    public object Coalesce(bool checkRightFirst = false)
    {
        if (checkRightFirst)
        {
            if (IsRight)
            {
                return Right!;
            }
            if (IsLeft)
            {
                return Left!;
            }
        }
        else
        {
            if (IsLeft)
            {
                return Left!;
            }
            if (IsRight)
            {
                return Right!;
            }
        }
        throw new InvalidOperationException("Both Left and Right are null.");
    }

    public void Deconstruct(out TLeft? left, out TRight? right)
    {
        left = Left;
        right = Right;
    }

    public void Deconstruct(out object leftOrRight, out bool isLeft)
    {
        leftOrRight = IsLeft ? Left! : Right!;
        isLeft = IsLeft;
    }

    public bool Equals(Either<TLeft, TRight> other)
    {
        if (IsLeft && other.IsLeft)
        {
            return EqualityComparer<TLeft>.Default.Equals(Left, other.Left);
        }

        if (IsRight && other.IsRight)
        {
            return EqualityComparer<TRight>.Default.Equals(Right, other.Right);
        }

        return false;
    }

    public override bool Equals(object? obj)
    {
        return obj is Either<TLeft, TRight> other && Equals(other);
    }

    public T Fold<T>(Func<TLeft, T> onLeft, Func<TRight, T> onRight)
    {
        return IsLeft ? onLeft(Left!) : onRight(Right!);
    }

    public override int GetHashCode()
    {
        return IsLeft ? Left?.GetHashCode() ?? 0 : Right?.GetHashCode() ?? 0;
    }

    public void IfLeft(Action<TLeft> action)
    {
        if (IsLeft) action(Left!);
    }

    public void IfRight(Action<TRight> action)
    {
        if (IsRight) action(Right!);
    }

    public Either<TLeft, TResult> Map<TResult>(Func<TRight, TResult> mapper)
        where TResult : notnull
    {
        if (mapper == null) throw new ArgumentNullException(nameof(mapper));
        return IsRight ? Either<TLeft, TResult>.CreateRight(mapper(Right!)) : Either<TLeft, TResult>.CreateLeft(Left!);
    }

    public Either<TResult, TRight> MapLeft<TResult>(Func<TLeft, TResult> func)
    {
        return IsLeft ? new Either<TResult, TRight>(func(Left!), default) : new Either<TResult, TRight>(default, Right);
    }

    public TResult Match<TResult>(Func<TLeft, TResult> left, Func<TRight, TResult> right)
    {
        return IsRight ? right(Right!) : left(Left!);
    }

    public async Task<TResult> MatchAsync<TResult>(
        Func<TLeft, Task<TResult>> onLeftFunc,
        Func<TRight, Task<TResult>> onRightFunc)
    {
        return IsLeft ? await onLeftFunc(Left!) : await onRightFunc(Right!);
    }

    public Either<TLeft, TRight> OrElse(Func<TRight> fallBackFunc)
    {
        return IsRight ? this : new Either<TLeft, TRight>(default, fallBackFunc());
    }

    public Either<TLeft, TResult> Select<TResult>(Func<TRight, TResult> selector)
    {
        return IsRight
            ? new Either<TLeft, TResult>(default, selector(Right!))
            : new Either<TLeft, TResult>(Left, default);
    }

    public Either<TLeft, TResult> SelectMany<TResult>(Func<TRight, Either<TLeft, TResult>> selector)
    {
        return IsRight ? selector(Right!) : new Either<TLeft, TResult>(Left, default);
    }

    public override string ToString()
    {
        return IsLeft ? $"Left: {Left}" : $"Right: {Right}";
    }
    public Either<TLeft, TRight> WhereLeft(Func<TLeft, bool> predicate)
    {
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));
        return IsLeft && predicate(Left!) ? this : new Either<TLeft, TRight>(default, Right);
    }

    public Either<TLeft, TRight> WhereRight(Func<TRight, bool> predicate)
    {
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));
        return IsRight && predicate(Right!) ? this : new Either<TLeft, TRight>(Left, default);
    }
}