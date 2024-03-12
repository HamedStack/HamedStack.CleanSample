// ReSharper disable UnusedMember.Global
namespace CleanSample.Framework.Domain.Functional.Choices;
public class ChoiceBase<T0, T1, T2, T3> : IChoice
{
    private readonly T0? _value0;
    private readonly T1? _value1;
    private readonly T2? _value2;
    private readonly T3? _value3;

    protected ChoiceBase(Choice<T0?, T1?, T2?, T3?> input)
    {
        Index = input.Index;
        switch (Index)
        {
            case 0: _value0 = input.AsT0; break;
            case 1: _value1 = input.AsT1; break;
            case 2: _value2 = input.AsT2; break;
            case 3: _value3 = input.AsT3; break;
            default: throw new InvalidOperationException();
        }
    }

    public object? Value =>
        Index switch
        {
            0 => _value0,
            1 => _value1,
            2 => _value2,
            3 => _value3,
            _ => throw new InvalidOperationException()
        };

    public int Index { get; }

    public bool IsT0 => Index == 0;
    public bool IsT1 => Index == 1;
    public bool IsT2 => Index == 2;
    public bool IsT3 => Index == 3;

    public T0? AsT0 =>
        Index == 0 ?
            _value0 :
            throw new InvalidOperationException($"Cannot return as T0 as result is T{Index}");
    public T1? AsT1 =>
        Index == 1 ?
            _value1 :
            throw new InvalidOperationException($"Cannot return as T1 as result is T{Index}");
    public T2? AsT2 =>
        Index == 2 ?
            _value2 :
            throw new InvalidOperationException($"Cannot return as T2 as result is T{Index}");
    public T3? AsT3 =>
        Index == 3 ?
            _value3 :
            throw new InvalidOperationException($"Cannot return as T3 as result is T{Index}");

        

    public void Switch(Action<T0?>? f0, Action<T1?>? f1, Action<T2?>? f2, Action<T3?>? f3)
    {
        switch (Index)
        {
            case 0 when f0 != null:
                f0(_value0);
                return;
            case 1 when f1 != null:
                f1(_value1);
                return;
            case 2 when f2 != null:
                f2(_value2);
                return;
            case 3 when f3 != null:
                f3(_value3);
                return;
            default:
                throw new InvalidOperationException();
        }
    }

    public TResult? Match<TResult>(Func<T0?, TResult?>? f0, Func<T1?, TResult?>? f1, Func<T2?, TResult?>? f2, Func<T3?, TResult?>? f3)
    {
        return Index switch
        {
            0 when f0 != null => f0(_value0),
            1 when f1 != null => f1(_value1),
            2 when f2 != null => f2(_value2),
            3 when f3 != null => f3(_value3),
            _ => throw new InvalidOperationException()
        };
    }

        

        

    public bool TryPickT0(out T0? value, out Choice<T1?, T2?, T3?> remainder)
    {
        value = IsT0 ? AsT0 : default;
        remainder = Index switch
        {
            0 => default,
            1 => AsT1,
            2 => AsT2,
            3 => AsT3,
            _ => throw new InvalidOperationException()
        };
        return IsT0;
    }
        
    public bool TryPickT1(out T1? value, out Choice<T0?, T2?, T3?> remainder)
    {
        value = IsT1 ? AsT1 : default;
        remainder = Index switch
        {
            0 => AsT0,
            1 => default,
            2 => AsT2,
            3 => AsT3,
            _ => throw new InvalidOperationException()
        };
        return IsT1;
    }
        
    public bool TryPickT2(out T2? value, out Choice<T0?, T1?, T3?> remainder)
    {
        value = IsT2 ? AsT2 : default;
        remainder = Index switch
        {
            0 => AsT0,
            1 => AsT1,
            2 => default,
            3 => AsT3,
            _ => throw new InvalidOperationException()
        };
        return IsT2;
    }
        
    public bool TryPickT3(out T3? value, out Choice<T0?, T1?, T2?> remainder)
    {
        value = IsT3 ? AsT3 : default;
        remainder = Index switch
        {
            0 => AsT0,
            1 => AsT1,
            2 => AsT2,
            3 => default,
            _ => throw new InvalidOperationException()
        };
        return IsT3;
    }

    private bool Equals(ChoiceBase<T0, T1, T2, T3> other) =>
        Index == other.Index &&
        Index switch
        {
            0 => Equals(_value0, other._value0),
            1 => Equals(_value1, other._value1),
            2 => Equals(_value2, other._value2),
            3 => Equals(_value3, other._value3),
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

        return obj is ChoiceBase<T0, T1, T2, T3> o && Equals(o);
    }

    public override string ToString() =>
        Index switch {
            0 => _value0.FormatValue(),
            1 => _value1.FormatValue(),
            2 => _value2.FormatValue(),
            3 => _value3.FormatValue(),
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
                2 => _value2?.GetHashCode(),
                3 => _value3?.GetHashCode(),
                _ => 0
            } ?? 0;
            return (hashCode*397) ^ Index;
        }
    }
}