// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

#pragma warning disable CS1591

using System.Diagnostics.CodeAnalysis;

namespace CleanSample.Framework.Domain.Functional;

public readonly struct Validation<TValue, TError> : IEquatable<Validation<TValue, TError>>
{
    private Validation(TError[] errors)
    {
        if (errors == null) throw new ArgumentNullException(nameof(errors));
        if (errors.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(errors));

        Errors = errors;
        IsValid = false;
    }

    private Validation([DisallowNull] TValue value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
        IsValid = true;
    }

    public TError[]? Errors { get; }
    public bool IsValid { get; }
    public TValue? Value { get; }

    public static Validation<TValue, TError> Create(bool condition, [DisallowNull] TError error,
        [DisallowNull] TValue value)
    {
        if (error == null) throw new ArgumentNullException(nameof(error));
        if (value == null) throw new ArgumentNullException(nameof(value));
        return condition ? Valid(value) : Invalid(new[] { error });
    }

    public static Validation<TValue, TError> Create(bool condition, TError[] errors, [DisallowNull] TValue value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));
        if (errors == null) throw new ArgumentNullException(nameof(errors));
        if (errors.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(errors));
        return condition ? Valid(value) : Invalid(errors);
    }

    public static implicit operator Validation<TValue, TError>([DisallowNull] TValue value)
    {
        return Valid(value);
    }

    public static implicit operator Validation<TValue, TError>([DisallowNull] TError error)
    {
        return Invalid(new[] { error });
    }

    public static implicit operator Validation<TValue, TError>(TError[] errors)
    {
        return Invalid(errors);
    }
    public static Validation<TValue, TError> Invalid(TError[] errors)
    {
        if (errors == null) throw new ArgumentNullException(nameof(errors));
        if (errors.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(errors));

        return new Validation<TValue, TError>(errors);
    }

    public static Validation<TValue, TError> Invalid([DisallowNull] TError error)
    {
        if (error == null) throw new ArgumentNullException(nameof(error));
        return new Validation<TValue, TError>(new[] { error });
    }

    public static Func<Validation<TValue, TError>, Validation<TResult, TError>> Lift<TResult>(
        Func<TValue, TResult> func) where TResult : notnull
    {
        return validation => validation.Map(func);
    }

    public static Validation<TValue, TError> Valid([DisallowNull] TValue value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));
        return new Validation<TValue, TError>(value);
    }

    public bool Equals(Validation<TValue, TError> other)
    {
        return IsValid == other.IsValid &&
               EqualityComparer<TValue?>.Default.Equals(Value, other.Value) &&
               ArrayEquals(Errors, other.Errors);
    }

    public override bool Equals(object? obj)
    {
        return obj is Validation<TValue, TError> other && Equals(other);
    }

    public IEnumerable<TError> GetErrors()
    {
        return Errors ?? Enumerable.Empty<TError>();
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hash = 17;

            hash = hash * 23 + IsValid.GetHashCode();

            if (Value != null)
            {
                hash = hash * 23 + Value.GetHashCode();
            }

            hash = hash * 23 + ArrayHashCode(Errors);

            return hash;
        }
    }

    public TValue GetOrElse(TValue defaultValue)
    {
        return IsValid ? Value! : defaultValue;
    }

    public Validation<TResult, TError> Map<TResult>(Func<TValue, TResult> mapper)
    {
        if (!IsValid) return Validation<TResult, TError>.Invalid(Errors!);
        var newValue = mapper(Value!)!;
        return Validation<TResult, TError>.Valid(newValue);
    }

    public TResult Match<TResult>(Func<TValue, TResult> onValid, Func<IEnumerable<TError>, TResult> onInvalid)
    {
        return IsValid ? onValid(Value!) : onInvalid(Errors ?? Enumerable.Empty<TError>());
    }

    public Validation<TResult, TError> Select<TResult>(Func<TValue, TResult> selector)
    {
        if (selector == null) throw new ArgumentNullException(nameof(selector));
        if (!IsValid)
            return Validation<TResult, TError>.Invalid(Errors!);

        return Validation<TResult, TError>.Valid(selector(Value!)!);
    }

    public Validation<TResult, TError> SelectMany<TResult>(
        Func<TValue, Validation<TResult, TError>> selector)
    {
        if (selector == null) throw new ArgumentNullException(nameof(selector));
        if (!IsValid)
            return Validation<TResult, TError>.Invalid(Errors!);

        return selector(Value!);
    }

    public Validation<TResult, TError> SelectMany<TIntermediate, TResult>(
        Func<TValue, Validation<TIntermediate, TError>> selector,
        Func<TValue, TIntermediate, TResult> resultSelector)
    {
        if (selector == null) throw new ArgumentNullException(nameof(selector));
        if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));
        if (!IsValid)
            return Validation<TResult, TError>.Invalid(Errors!);

        var intermediateValidation = selector(Value!);
        return intermediateValidation.IsValid
            ? Validation<TResult, TError>.Valid(resultSelector(Value!, intermediateValidation.Value!)!)
            : Validation<TResult, TError>.Invalid(intermediateValidation.Errors!);
    }

    public override string ToString()
    {
        return IsValid ? $"Valid: {Value}" : $"Invalid: {string.Join(", ", GetErrors())}";
    }

    public Validation<TValue, TError> Validate(Func<TValue, bool> condition, [DisallowNull] TError error)
    {
        if (Value != null && IsValid && !condition(Value))
            return Invalid(new[] { error });

        return this;
    }

    public Validation<TValue, TError> Validate(Func<TValue, bool> condition, TError[] errors)
    {
        if (Value != null && IsValid && !condition(Value))
            return Invalid(errors);

        return this;
    }
    public Validation<TValue, TError> Where(Func<TValue, bool> predicate, TError? error)
    {
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));
        if (IsValid && !predicate(Value!) && error != null)
            return Invalid(new[] { error });

        return this;
    }
    private static bool ArrayEquals<T>(T[]? array1, T[]? array2)
    {
        if (ReferenceEquals(array1, array2)) return true;
        if (array1 is null || array2 is null) return false;

        if (array1.Length != array2.Length) return false;
        return !array1.Where((t, i) => !EqualityComparer<T>.Default.Equals(t, array2[i])).Any();
    }

    private static int ArrayHashCode<T>(T[]? array)
    {
        if (array is null) return 0;
        return array.Aggregate(17, (current, element) => current * 23 + (element?.GetHashCode() ?? 0));
    }
}