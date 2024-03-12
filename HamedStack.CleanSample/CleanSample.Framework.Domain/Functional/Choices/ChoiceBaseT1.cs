// ReSharper disable UnusedMember.Global
namespace CleanSample.Framework.Domain.Functional.Choices;
public class ChoiceBase<T0, T1> : IChoice
{
    private readonly T0? _value0;
    private readonly T1? _value1;

    protected ChoiceBase(Choice<T0?, T1?> input)
    {
        Index = input.Index;
        switch (Index)
        {
            case 0: _value0 = input.AsT0; break;
            case 1: _value1 = input.AsT1; break;
            default: throw new InvalidOperationException();
        }
    }

    public object? Value =>
        Index switch
        {
            0 => _value0,
            1 => _value1,
            _ => throw new InvalidOperationException()
        };

    public int Index { get; }

    public bool IsT0 => Index == 0;
    public bool IsT1 => Index == 1;

    public T0? AsT0 =>
        Index == 0 ?
            _value0 :
            throw new InvalidOperationException($"Cannot return as T0 as result is T{Index}");
    public T1? AsT1 =>
        Index == 1 ?
            _value1 :
            throw new InvalidOperationException($"Cannot return as T1 as result is T{Index}");

        

    public void Switch(Action<T0?>? f0, Action<T1?>? f1)
    {
        switch (Index)
        {
            case 0 when f0 != null:
                f0(_value0);
                return;
            case 1 when f1 != null:
                f1(_value1);
                return;
            default:
                throw new InvalidOperationException();
        }
    }

    public TResult? Match<TResult>(Func<T0?, TResult?>? f0, Func<T1?, TResult?>? f1)
    {
        return Index switch
        {
            0 when f0 != null => f0(_value0),
            1 when f1 != null => f1(_value1),
            _ => throw new InvalidOperationException()
        };
    }

        

        

    public bool TryPickT0(out T0? value, out T1? remainder)
    {
        value = IsT0 ? AsT0 : default;
        remainder = Index switch
        {
            0 => default,
            1 => AsT1,
            _ => throw new InvalidOperationException()
        };
        return IsT0;
    }
        
    public bool TryPickT1(out T1? value, out T0? remainder)
    {
        value = IsT1 ? AsT1 : default;
        remainder = Index switch
        {
            0 => AsT0,
            1 => default,
            _ => throw new InvalidOperationException()
        };
        return IsT1;
    }

    private bool Equals(ChoiceBase<T0, T1> other) =>
        Index == other.Index &&
        Index switch
        {
            0 => Equals(_value0, other._value0),
            1 => Equals(_value1, other._value1),
            _ => false
        };

    public override bool Equals(object? obj)
    {
        switch (obj)
        {
            case null:
                return false;
        }

        if (ReferenceEquals(this, obj)) {
            return true;
        }

        return obj is ChoiceBase<T0, T1> o && Equals(o);
    }

    public override string ToString() =>
        Index switch {
            0 => _value0.FormatValue(),
            1 => _value1.FormatValue(),
            _ => throw new InvalidOperationException("Unexpected index, which indicates a problem in the Choice implementation.")
        };

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = Index switch
            {
                0 => _value0?.GetHashCode(),
                1 => _value1?.GetHashCode(),
                _ => 0
            } ?? 0;
            return (hashCode*397) ^ Index;
        }
    }
}