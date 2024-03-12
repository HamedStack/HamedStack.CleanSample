// ReSharper disable UnusedType.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
#pragma warning disable CS1591

using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace CleanSample.Framework.Domain.Functional;

public readonly struct Option<T> : IEquatable<Option<T>>
{
    private readonly T? _value;

    private Option(T? value)
    {
        _value = value;
    }

    public bool IsNone => !IsSome;

    public bool IsSome => _value is not null;

    public static Option<T> IfSome(bool condition, T value)
    {
        return condition ? Some(value!) : None();
    }

    public static Option<T> IfSome(Func<T, bool> predicate, T value)
    {
        return predicate(value) ? Some(value!) : None();
    }

    public static Task<Option<T>> IfSomeAsync(bool condition, T value)
    {
        return Task.FromResult(condition ? Some(value!) : None());
    }

    public static async Task<Option<T>> IfSomeAsync(Func<T, Task<bool>> predicate, T value)
    {
        return await predicate(value) ? Some(value!) : None();
    }

    public static implicit operator Option<T>(T? value)
    {
        return value is not null ? Some(value) : None();
    }

    public static Option<T> None()
    {
        return new Option<T>(default);
    }

    public static bool operator !=(Option<T> left, Option<T> right)
    {
        return !(left == right);
    }

    public static bool operator <(Option<T> left, Option<T> right)
    {
        if (left.IsNone && right.IsNone) return false;

        if (left.IsSome && right.IsSome)
            return Comparer<T>.Default.Compare(left._value, right._value) < 0;

        return left.IsNone;
    }

    public static bool operator <=(Option<T> left, Option<T> right)
    {
        return !(right < left);
    }

    public static bool operator ==(Option<T> left, Option<T> right)
    {
        return left.Equals(right);
    }

    public static bool operator >(Option<T> left, Option<T> right)
    {
        return right < left;
    }

    public static bool operator >=(Option<T> left, Option<T> right)
    {
        return !(left < right);
    }

    public static Option<T> Some([DisallowNull] T value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));
        return new Option<T>(value);
    }

    public static Option<(T, TOther)> Zip<TOther>(Option<T> option1, Option<TOther> option2)
    {
        if (option1.IsSome && option2.IsSome)
        {
            return Option<(T, TOther)>.Some((option1._value!, option2._value!));
        }

        return Option<(T, TOther)>.None();
    }

    public IEnumerable<T> AsEnumerable()
    {
        if (IsSome) yield return _value!;
    }

    public Option<TResult> Bind<TResult>(Func<T, Option<TResult>> binder)
    {
        return IsSome ? binder(_value!) : Option<TResult>.None();
    }

    public async Task<Option<TResult>> BindAsync<TResult>(Func<T, Task<Option<TResult>>> asyncBinder)
    {
        return IsSome ? await asyncBinder(_value!) : Option<TResult>.None();
    }

    public Option<TResult> Cast<TResult>()
    {
        return IsSome && _value is TResult v
            ? Option<TResult>.Some(v)
            : Option<TResult>.None();
    }

    public Option<(T, T)> Combine(Option<T> other)
    {
        if (IsSome && other.IsSome) return Option<(T, T)>.Some((_value!, other._value!));

        return Option<(T, T)>.None();
    }

    public bool Contains(T value)
    {
        return IsSome && EqualityComparer<T>.Default.Equals(_value, value);
    }

    public void Deconstruct(out T? value)
    {
        value = _value;
    }

    public void Deconstruct(out bool isSome, out T? value)
    {
        isSome = IsSome;
        value = _value;
    }

    public bool Equals(Option<T> other)
    {
        if (IsNone && other.IsNone) return true;

        if (IsSome && other.IsSome)
            // If T is a value type, you can use EqualityComparer<T>.Default.Equals
            // For simplicity, assuming T is a reference type here
            return EqualityComparer<T>.Default.Equals(_value, other._value);

        return false;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Option<T> other) return Equals(other);

        return false;
    }

    public bool Equals(Option<T> other, IEqualityComparer<T> comparer)
    {
        if (IsNone && other.IsNone) return true;

        if (IsSome && other.IsSome) return comparer.Equals(_value, other._value);

        return false;
    }

    public override int GetHashCode()
    {
        return IsSome ? HashCode.Combine(_value) : 0;
    }
    public void IfNone(Action action)
    {
        if (IsNone) action();
    }

    public async Task IfNoneAsync(Func<Task> asyncAction)
    {
        if (IsNone) await asyncAction();
    }

    public void IfSome(Action<T> action)
    {
        if (IsSome) action(_value!);
    }

    public async Task IfSomeAsync(Func<T, Task> asyncAction)
    {
        if (IsSome) await asyncAction(_value!);
    }

    public Option<TResult> Map<TResult>(Func<T, TResult> mapper)
        where TResult : notnull
    {
        return _value is null ? Option<TResult>.None() : Option<TResult>.Some(mapper(_value));
    }

    public async Task<Option<TResult>> MapAsync<TResult>(Func<T, Task<TResult>> asyncMapper)
    {
        return IsSome ? Option<TResult>.Some((await asyncMapper(_value!))!) : Option<TResult>.None();
    }

    public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none)
                                where TResult : notnull
    {
        return _value is null ? none() : some(_value);
    }
    public async Task<TResult> MatchAsync<TResult>(Func<T, Task<TResult>> someAsync, Func<Task<TResult>> noneAsync)
    {
        return IsSome ? await someAsync(_value!) : await noneAsync();
    }

    public Option<T> OrElse(Option<T> alternative)
    {
        return IsSome ? this : alternative;
    }

    public Option<T> OrElse(Func<Option<T>> alternativeProvider)
    {
        return IsSome ? this : alternativeProvider();
    }

    public T OrElseGet(Func<T> fallBackFunc)
    {
        return IsSome ? _value! : fallBackFunc();
    }

    public T OrElseThrow(Func<Exception> exceptionProvider)
    {
        if (IsSome) return _value!;

        throw exceptionProvider();
    }

    public T OrElseThrow(Exception ex)
    {
        return IsSome ? _value! : throw ex;
    }

    public Option<TResult> Select<TResult>(Func<T, TResult> selector)
    {
        return IsSome ? Option<TResult>.Some(selector(_value!)!) : Option<TResult>.None();
    }

    public Option<TResult> SelectMany<TResult>(Func<T, Option<TResult>> selector)
    {
        return IsSome ? selector(_value!) : Option<TResult>.None();
    }

    public T[] ToArray()
    {
        return IsSome ? new[] { _value! } : Array.Empty<T>();
    }

    public List<T> ToList()
    {
        return IsSome ? new List<T> { _value! } : new List<T>();
    }
    public override string ToString()
    {
        return IsSome ? $"Some: {_value}" : nameof(None);
    }

    public TResult Transform<TResult>(Func<Option<T>, TResult> transformer)
    {
        return transformer(this);
    }

    public Option<T> Transform(Func<T, T> transformer)
    {
        return IsSome ? Some(transformer(_value!) ?? throw new InvalidOperationException()) : this;
    }

    public T Unwrap()
    {
        if (IsNone)
            throw new InvalidOperationException("Attempted to unwrap an Option.None.");

        return _value!;
    }
    public T Unwrap(T defaultValue)
    {
        return IsSome ? _value! : defaultValue;
    }

    public T Unwrap(Func<T> defaultValueFunc)
    {
        return IsSome ? _value! : defaultValueFunc();
    }
    public Option<T> Validate(Func<T?, bool> validation)
    {
        return validation(_value) ? this : None();
    }

    public Option<T> Where(Func<T, bool> predicate)
    {
        return IsSome && predicate(_value!) ? this : None();
    }

    public Option<T> Where(Expression<Func<T, bool>> expression)
    {
        var compiledExpression = expression.Compile();
        return IsSome && compiledExpression(_value!) ? this : None();
    }

    public async Task<Option<T>> WhereAsync(Func<T, Task<bool>> predicate)
    {
        return IsSome && await predicate(_value!) ? this : None();
    }
    public async Task<Option<T>> WhereAsync(Expression<Func<T, Task<bool>>> expression)
    {
        var compiledExpression = expression.Compile();
        return IsSome && await compiledExpression(_value!) ? this : None();
    }
}