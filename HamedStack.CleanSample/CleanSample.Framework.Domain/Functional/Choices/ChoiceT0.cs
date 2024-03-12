// ReSharper disable UnusedMember.Global
namespace CleanSample.Framework.Domain.Functional.Choices;
public readonly struct Choice<T0> : IChoice
{
    private readonly T0? _value0;

    private Choice(int index, T0? value0 = default)
    {
        Index = index;
        _value0 = value0;
    }

    public object? Value =>
        Index switch
        {
            0 => _value0,
            _ => throw new InvalidOperationException()
        };

    public int Index { get; }

    public bool IsT0 => Index == 0;

    public T0? AsT0 =>
        Index == 0 ?
            _value0 :
            throw new InvalidOperationException($"Cannot return as T0 as result is T{Index}");

    public static implicit operator Choice<T0>(T0? t) => new(0, value0: t);

    public void Switch(Action<T0?>? f0)
    {
        switch (Index)
        {
            case 0 when f0 != null:
                f0(_value0);
                return;
            default:
                throw new InvalidOperationException();
        }
    }

    public TResult? Match<TResult>(Func<T0?, TResult?>? f0)
    {
        return Index switch
        {
            0 when f0 != null => f0(_value0),
            _ => throw new InvalidOperationException()
        };
    }

    public static Choice<T0?> FromT0(T0? input) => input;

        
    public Choice<TResult?> MapT0<TResult>(Func<T0?, TResult?> mapFunc)
    {
        return mapFunc switch
        {
            null => throw new ArgumentNullException(nameof(mapFunc)),
            _ => Index switch
            {
                0 => mapFunc(AsT0),
                _ => throw new InvalidOperationException()
            }
        };
    }

    private bool Equals(Choice<T0> other) =>
        Index == other.Index &&
        Index switch
        {
            0 => Equals(_value0, other._value0),
            _ => false
        };

    public override bool Equals(object? obj)
    {
        return obj switch
        {
            null => false,
            _ => obj is Choice<T0> o && Equals(o)
        };
    }

    public override string ToString() =>
        Index switch {
            0 => _value0.FormatValue(),
            _ => throw new InvalidOperationException("Unexpected index, which indicates a problem in the Choice implementation.")
        };

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = Index switch
            {
                0 => _value0?.GetHashCode(),
                _ => 0
            } ?? 0;
            return (hashCode*397) ^ Index;
        }
    }
}