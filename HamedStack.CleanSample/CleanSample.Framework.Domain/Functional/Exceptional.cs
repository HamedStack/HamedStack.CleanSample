// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

using System.Diagnostics.CodeAnalysis;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace CleanSample.Framework.Domain.Functional;

public readonly struct Exceptional<T> : IEquatable<Exceptional<T>>
{
    private Exceptional([DisallowNull] T value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
        HasValue = true;
        HasException = false;
    }

    private Exceptional(Exception exception)
    {
        Exception = exception ?? throw new ArgumentNullException(nameof(exception));
        HasValue = false;
        HasException = true;
    }

    public Exception? Exception { get; }
    public bool HasException { get; }
    public bool HasValue { get; }
    public T? Value { get; }
    public static Exceptional<T> Failure(Exception exception)
    {
        if (exception == null) throw new ArgumentNullException(nameof(exception));
        return new Exceptional<T>(exception);
    }

    public static implicit operator Exceptional<T>([DisallowNull] T value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));
        return new Exceptional<T>(value);
    }

    public static implicit operator Exceptional<T>(Exception ex)
    {
        if (ex == null) throw new ArgumentNullException(nameof(ex));
        return new Exceptional<T>(ex);
    }

    public static bool operator !=(Exceptional<T> left, Exceptional<T> right)
    {
        return !(left == right);
    }

    public static bool operator ==(Exceptional<T> left, Exceptional<T> right)
    {
        return left.Equals(right);
    }

    public static Exceptional<T> Success([DisallowNull] T value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));
        return new Exceptional<T>(value);
    }
    public static Exceptional<T> TryCatch(Func<T> func)
    {
        if (func == null) throw new ArgumentNullException(nameof(func));
        try
        {
            return new Exceptional<T>(func()!);
        }
        catch (Exception ex)
        {
            return new Exceptional<T>(ex);
        }
    }

    public void Deconstruct(out bool hasValue, out T? value, out Exception? exception)
    {
        hasValue = HasValue;
        value = Value;
        exception = Exception;
    }

    public void Deconstruct(out T? value, out Exception? exception)
    {
        value = Value;
        exception = Exception;
    }

    public bool Equals(Exceptional<T> other)
    {
        if (HasValue && other.HasValue)
        {
            return EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        if (!HasValue && !other.HasValue)
        {
            return Equals(Exception, other.Exception);
        }

        return false;
    }

    public override bool Equals(object? obj)
    {
        return obj is Exceptional<T> other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HasValue ? Value?.GetHashCode() ?? 0 : Exception?.GetHashCode() ?? 0;
    }

    public void IfException(Action<Exception> action)
    {
        if (Exception is not null) action(Exception);
    }

    public void IfValue(Action<T> action)
    {
        if (HasValue) action(Value!);
    }

    public Exceptional<TResult> Map<TResult>(Func<T, TResult> mapper)
    {
        return HasValue ? Exceptional<TResult>.Success(mapper(Value!)!) : Exceptional<TResult>.Failure(Exception!);
    }

    public Exceptional<TNew> MapValue<TNew>(Func<T, TNew> mapper)
    {
        if (mapper == null) throw new ArgumentNullException(nameof(mapper));

        return HasValue
            ? new Exceptional<TNew>(mapper(Value!) ?? throw new InvalidOperationException())
            : Exceptional<TNew>.Failure(Exception!);
    }

    public TResult Match<TResult>(Func<T, TResult> success, Func<Exception, TResult> failure)
    {
        return HasValue ? success(Value!) : failure(Exception!);
    }
    public Exceptional<TResult> Select<TResult>(Func<T, TResult> selector)
    {
        if (selector == null) throw new ArgumentNullException(nameof(selector));
        return HasValue ? Exceptional<TResult>.Success(selector(Value!)!) : Exceptional<TResult>.Failure(Exception!);
    }

    public Exceptional<TResult> SelectMany<TResult>(Func<T, Exceptional<TResult>> selector)
    {
        if (selector == null) throw new ArgumentNullException(nameof(selector));
        return HasValue ? selector(Value!) : Exceptional<TResult>.Failure(Exception!);
    }

    public Exceptional<T> Tap(Action<T> action)
    {
        if (HasValue) action(Value!);
        return this;
    }

    public override string ToString()
    {
        return HasException ? $"Exception: {Exception?.Message}" : $"Value: {Value}";
    }

    public Exceptional<TResult> Transform<TResult>(
        Func<T, TResult> transformValue,
        Func<Exception, Exception> transformException)
    {
        if (transformValue == null) throw new ArgumentNullException(nameof(transformValue));
        if (transformException == null) throw new ArgumentNullException(nameof(transformException));
        return HasValue
            ? new Exceptional<TResult>(transformValue(Value!)!)
            : new Exceptional<TResult>(transformException(Exception!));
    }

    public T ValueOrThrow()
    {
        if (HasValue) return Value!;
        throw new InvalidOperationException($"No value present. Exception: {Exception?.Message}");
    }
    public Exceptional<T> Where(Func<T, bool> predicate)
    {
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));
        return HasValue && predicate(Value!) ? this : Failure(Exception!);
    }

    public Exceptional<TResult> Cast<TResult>()
    {
        return HasValue && Value is TResult v
            ? Exceptional<TResult>.Success(v)
            : Exceptional<TResult>.Failure(Exception!);
    }
}