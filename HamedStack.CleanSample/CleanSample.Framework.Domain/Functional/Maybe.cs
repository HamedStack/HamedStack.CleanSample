// ReSharper disable UnusedType.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
#pragma warning disable CS1591

using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace CleanSample.Framework.Domain.Functional;

public readonly struct Maybe<T> : IEquatable<Maybe<T>>
{
    private readonly T? _value;

    private Maybe(T? value)
    {
        _value = value;
    }

    public bool IsNothing => !IsJust;

    public bool IsJust => _value is not null;

    public static Maybe<T> IfJust(bool condition, T value)
    {
        return condition ? Just(value!) : Nothing();
    }

    public static Maybe<T> IfJust(Func<T, bool> predicate, T value)
    {
        return predicate(value) ? Just(value!) : Nothing();
    }

    public static Task<Maybe<T>> IfJustAsync(bool condition, T value)
    {
        return Task.FromResult(condition ? Just(value!) : Nothing());
    }

    public static async Task<Maybe<T>> IfJustAsync(Func<T, Task<bool>> predicate, T value)
    {
        return await predicate(value) ? Just(value!) : Nothing();
    }

    public static implicit operator Maybe<T>(T? value)
    {
        return value is not null ? Just(value) : Nothing();
    }

    public static Maybe<T> Nothing()
    {
        return new Maybe<T>(default);
    }

    public static bool operator !=(Maybe<T> left, Maybe<T> right)
    {
        return !(left == right);
    }

    public static bool operator <(Maybe<T> left, Maybe<T> right)
    {
        if (left.IsNothing && right.IsNothing) return false;

        if (left.IsJust && right.IsJust)
            return Comparer<T>.Default.Compare(left._value, right._value) < 0;

        return left.IsNothing;
    }

    public static bool operator <=(Maybe<T> left, Maybe<T> right)
    {
        return !(right < left);
    }

    public static bool operator ==(Maybe<T> left, Maybe<T> right)
    {
        return left.Equals(right);
    }

    public static bool operator >(Maybe<T> left, Maybe<T> right)
    {
        return right < left;
    }

    public static bool operator >=(Maybe<T> left, Maybe<T> right)
    {
        return !(left < right);
    }

    public static Maybe<T> Just([DisallowNull] T value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));
        return new Maybe<T>(value);
    }

    public static Maybe<(T, TOther)> Zip<TOther>(Maybe<T> option1, Maybe<TOther> option2)
    {
        if (option1.IsJust && option2.IsJust)
        {
            return Maybe<(T, TOther)>.Just((option1._value!, option2._value!));
        }

        return Maybe<(T, TOther)>.Nothing();
    }

    public IEnumerable<T> AsEnumerable()
    {
        if (IsJust) yield return _value!;
    }

    public Maybe<TResult> Bind<TResult>(Func<T, Maybe<TResult>> binder)
    {
        return IsJust ? binder(_value!) : Maybe<TResult>.Nothing();
    }

    public async Task<Maybe<TResult>> BindAsync<TResult>(Func<T, Task<Maybe<TResult>>> asyncBinder)
    {
        return IsJust ? await asyncBinder(_value!) : Maybe<TResult>.Nothing();
    }

    public Maybe<TResult> Cast<TResult>()
    {
        return IsJust && _value is TResult v
            ? Maybe<TResult>.Just(v)
            : Maybe<TResult>.Nothing();
    }

    public Maybe<(T, T)> Combine(Maybe<T> other)
    {
        if (IsJust && other.IsJust) return Maybe<(T, T)>.Just((_value!, other._value!));

        return Maybe<(T, T)>.Nothing();
    }

    public bool Contains(T value)
    {
        return IsJust && EqualityComparer<T>.Default.Equals(_value, value);
    }

    public void Deconstruct(out T? value)
    {
        value = _value;
    }

    public void Deconstruct(out bool isJust, out T? value)
    {
        isJust = IsJust;
        value = _value;
    }

    public bool Equals(Maybe<T> other)
    {
        if (IsNothing && other.IsNothing) return true;

        if (IsJust && other.IsJust)
            // If T is a value type, you can use EqualityComparer<T>.Default.Equals
            // For simplicity, assuming T is a reference type here
            return EqualityComparer<T>.Default.Equals(_value, other._value);

        return false;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Maybe<T> other) return Equals(other);

        return false;
    }

    public bool Equals(Maybe<T> other, IEqualityComparer<T> comparer)
    {
        if (IsNothing && other.IsNothing) return true;

        if (IsJust && other.IsJust) return comparer.Equals(_value, other._value);

        return false;
    }

    public override int GetHashCode()
    {
        return IsJust ? HashCode.Combine(_value) : 0;
    }
    public void IfNothing(Action action)
    {
        if (IsNothing) action();
    }

    public async Task IfNothingAsync(Func<Task> asyncAction)
    {
        if (IsNothing) await asyncAction();
    }

    public void IfJust(Action<T> action)
    {
        if (IsJust) action(_value!);
    }

    public async Task IfJustAsync(Func<T, Task> asyncAction)
    {
        if (IsJust) await asyncAction(_value!);
    }

    public Maybe<TResult> Map<TResult>(Func<T, TResult> mapper)
        where TResult : notnull
    {
        return _value is null ? Maybe<TResult>.Nothing() : Maybe<TResult>.Just(mapper(_value));
    }

    public async Task<Maybe<TResult>> MapAsync<TResult>(Func<T, Task<TResult>> asyncMapper)
    {
        return IsJust ? Maybe<TResult>.Just((await asyncMapper(_value!))!) : Maybe<TResult>.Nothing();
    }

    public TResult Match<TResult>(Func<T, TResult> just, Func<TResult> nothing)
                                where TResult : notnull
    {
        return _value is null ? nothing() : just(_value);
    }
    public async Task<TResult> MatchAsync<TResult>(Func<T, Task<TResult>> justAsync, Func<Task<TResult>> nothingAsync)
    {
        return IsJust ? await justAsync(_value!) : await nothingAsync();
    }

    public Maybe<T> OrElse(Maybe<T> alternative)
    {
        return IsJust ? this : alternative;
    }

    public Maybe<T> OrElse(Func<Maybe<T>> alternativeProvider)
    {
        return IsJust ? this : alternativeProvider();
    }

    public T OrElseGet(Func<T> fallBackFunc)
    {
        return IsJust ? _value! : fallBackFunc();
    }

    public T OrElseThrow(Func<Exception> exceptionProvider)
    {
        if (IsJust) return _value!;

        throw exceptionProvider();
    }

    public T OrElseThrow(Exception ex)
    {
        return IsJust ? _value! : throw ex;
    }

    public Maybe<TResult> Select<TResult>(Func<T, TResult> selector)
    {
        return IsJust ? Maybe<TResult>.Just(selector(_value!)!) : Maybe<TResult>.Nothing();
    }

    public Maybe<TResult> SelectMany<TResult>(Func<T, Maybe<TResult>> selector)
    {
        return IsJust ? selector(_value!) : Maybe<TResult>.Nothing();
    }

    public T[] ToArray()
    {
        return IsJust ? new[] { _value! } : Array.Empty<T>();
    }

    public List<T> ToList()
    {
        return IsJust ? new List<T> { _value! } : new List<T>();
    }
    public override string ToString()
    {
        return IsJust ? $"Just: {_value}" : nameof(Nothing);
    }

    public TResult Transform<TResult>(Func<Maybe<T>, TResult> transformer)
    {
        return transformer(this);
    }

    public Maybe<T> Transform(Func<T, T> transformer)
    {
        return IsJust ? Just(transformer(_value!) ?? throw new InvalidOperationException()) : this;
    }

    public T Unwrap()
    {
        if (IsNothing)
            throw new InvalidOperationException("Attempted to unwrap an Maybe.Nothing.");

        return _value!;
    }
    public T Unwrap(T defaultValue)
    {
        return IsJust ? _value! : defaultValue;
    }

    public T Unwrap(Func<T> defaultValueFunc)
    {
        return IsJust ? _value! : defaultValueFunc();
    }
    public Maybe<T> Validate(Func<T?, bool> validation)
    {
        return validation(_value) ? this : Nothing();
    }

    public Maybe<T> Where(Func<T, bool> predicate)
    {
        return IsJust && predicate(_value!) ? this : Nothing();
    }

    public Maybe<T> Where(Expression<Func<T, bool>> expression)
    {
        var compiledExpression = expression.Compile();
        return IsJust && compiledExpression(_value!) ? this : Nothing();
    }

    public async Task<Maybe<T>> WhereAsync(Func<T, Task<bool>> predicate)
    {
        return IsJust && await predicate(_value!) ? this : Nothing();
    }
    public async Task<Maybe<T>> WhereAsync(Expression<Func<T, Task<bool>>> expression)
    {
        var compiledExpression = expression.Compile();
        return IsJust && await compiledExpression(_value!) ? this : Nothing();
    }
}