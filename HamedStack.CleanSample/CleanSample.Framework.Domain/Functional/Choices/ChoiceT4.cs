// ReSharper disable UnusedMember.Global
namespace CleanSample.Framework.Domain.Functional.Choices;
public readonly struct Choice<T0, T1, T2, T3, T4> : IChoice
{
    private readonly T0? _value0;
    private readonly T1? _value1;
    private readonly T2? _value2;
    private readonly T3? _value3;
    private readonly T4? _value4;

    private Choice(int index, T0? value0 = default, T1? value1 = default, T2? value2 = default, T3? value3 = default, T4? value4 = default)
    {
        Index = index;
        _value0 = value0;
        _value1 = value1;
        _value2 = value2;
        _value3 = value3;
        _value4 = value4;
    }

    public object? Value =>
        Index switch
        {
            0 => _value0,
            1 => _value1,
            2 => _value2,
            3 => _value3,
            4 => _value4,
            _ => throw new InvalidOperationException()
        };

    public int Index { get; }

    public bool IsT0 => Index == 0;
    public bool IsT1 => Index == 1;
    public bool IsT2 => Index == 2;
    public bool IsT3 => Index == 3;
    public bool IsT4 => Index == 4;

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
    public T4? AsT4 =>
        Index == 4 ?
            _value4 :
            throw new InvalidOperationException($"Cannot return as T4 as result is T{Index}");

    public static implicit operator Choice<T0, T1, T2, T3, T4>(T0? t) => new(0, value0: t);
    public static implicit operator Choice<T0, T1, T2, T3, T4>(T1? t) => new(1, value1: t);
    public static implicit operator Choice<T0, T1, T2, T3, T4>(T2? t) => new(2, value2: t);
    public static implicit operator Choice<T0, T1, T2, T3, T4>(T3? t) => new(3, value3: t);
    public static implicit operator Choice<T0, T1, T2, T3, T4>(T4? t) => new(4, value4: t);

    public void Switch(Action<T0?>? f0, Action<T1?>? f1, Action<T2?>? f2, Action<T3?>? f3, Action<T4?>? f4)
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
            case 4 when f4 != null:
                f4(_value4);
                return;
            default:
                throw new InvalidOperationException();
        }
    }

    public TResult? Match<TResult>(Func<T0?, TResult?>? f0, Func<T1?, TResult?>? f1, Func<T2?, TResult?>? f2, Func<T3?, TResult?>? f3, Func<T4?, TResult?>? f4)
    {
        return Index switch
        {
            0 when f0 != null => f0(_value0),
            1 when f1 != null => f1(_value1),
            2 when f2 != null => f2(_value2),
            3 when f3 != null => f3(_value3),
            4 when f4 != null => f4(_value4),
            _ => throw new InvalidOperationException()
        };
    }

    public static Choice<T0?, T1?, T2?, T3?, T4?> FromT0(T0? input) => input;
    public static Choice<T0?, T1?, T2?, T3?, T4?> FromT1(T1? input) => input;
    public static Choice<T0?, T1?, T2?, T3?, T4?> FromT2(T2? input) => input;
    public static Choice<T0?, T1?, T2?, T3?, T4?> FromT3(T3? input) => input;
    public static Choice<T0?, T1?, T2?, T3?, T4?> FromT4(T4? input) => input;

        
    public Choice<TResult?, T1, T2, T3, T4> MapT0<TResult>(Func<T0?, TResult?> mapFunc)
    {
        return mapFunc switch
        {
            null => throw new ArgumentNullException(nameof(mapFunc)),
            _ => Index switch
            {
                0 => mapFunc(AsT0),
                1 => AsT1,
                2 => AsT2,
                3 => AsT3,
                4 => AsT4,
                _ => throw new InvalidOperationException()
            }
        };
    }
            
    public Choice<T0, TResult?, T2, T3, T4> MapT1<TResult>(Func<T1?, TResult?> mapFunc)
    {
        return mapFunc switch
        {
            null => throw new ArgumentNullException(nameof(mapFunc)),
            _ => Index switch
            {
                0 => AsT0,
                1 => mapFunc(AsT1),
                2 => AsT2,
                3 => AsT3,
                4 => AsT4,
                _ => throw new InvalidOperationException()
            }
        };
    }
            
    public Choice<T0, T1, TResult?, T3, T4> MapT2<TResult>(Func<T2?, TResult?> mapFunc)
    {
        return mapFunc switch
        {
            null => throw new ArgumentNullException(nameof(mapFunc)),
            _ => Index switch
            {
                0 => AsT0,
                1 => AsT1,
                2 => mapFunc(AsT2),
                3 => AsT3,
                4 => AsT4,
                _ => throw new InvalidOperationException()
            }
        };
    }
            
    public Choice<T0, T1, T2, TResult?, T4> MapT3<TResult>(Func<T3?, TResult?> mapFunc)
    {
        return mapFunc switch
        {
            null => throw new ArgumentNullException(nameof(mapFunc)),
            _ => Index switch
            {
                0 => AsT0,
                1 => AsT1,
                2 => AsT2,
                3 => mapFunc(AsT3),
                4 => AsT4,
                _ => throw new InvalidOperationException()
            }
        };
    }
            
    public Choice<T0, T1, T2, T3, TResult?> MapT4<TResult>(Func<T4?, TResult?> mapFunc)
    {
        return mapFunc switch
        {
            null => throw new ArgumentNullException(nameof(mapFunc)),
            _ => Index switch
            {
                0 => AsT0,
                1 => AsT1,
                2 => AsT2,
                3 => AsT3,
                4 => mapFunc(AsT4),
                _ => throw new InvalidOperationException()
            }
        };
    }

    public bool TryPickT0(out T0? value, out Choice<T1?, T2?, T3?, T4?> remainder)
    {
        value = IsT0 ? AsT0 : default;
        remainder = Index switch
        {
            0 => default,
            1 => AsT1,
            2 => AsT2,
            3 => AsT3,
            4 => AsT4,
            _ => throw new InvalidOperationException()
        };
        return IsT0;
    }
        
    public bool TryPickT1(out T1? value, out Choice<T0?, T2?, T3?, T4?> remainder)
    {
        value = IsT1 ? AsT1 : default;
        remainder = Index switch
        {
            0 => AsT0,
            1 => default,
            2 => AsT2,
            3 => AsT3,
            4 => AsT4,
            _ => throw new InvalidOperationException()
        };
        return IsT1;
    }
        
    public bool TryPickT2(out T2? value, out Choice<T0?, T1?, T3?, T4?> remainder)
    {
        value = IsT2 ? AsT2 : default;
        remainder = Index switch
        {
            0 => AsT0,
            1 => AsT1,
            2 => default,
            3 => AsT3,
            4 => AsT4,
            _ => throw new InvalidOperationException()
        };
        return IsT2;
    }
        
    public bool TryPickT3(out T3? value, out Choice<T0?, T1?, T2?, T4?> remainder)
    {
        value = IsT3 ? AsT3 : default;
        remainder = Index switch
        {
            0 => AsT0,
            1 => AsT1,
            2 => AsT2,
            3 => default,
            4 => AsT4,
            _ => throw new InvalidOperationException()
        };
        return IsT3;
    }
        
    public bool TryPickT4(out T4? value, out Choice<T0?, T1?, T2?, T3?> remainder)
    {
        value = IsT4 ? AsT4 : default;
        remainder = Index switch
        {
            0 => AsT0,
            1 => AsT1,
            2 => AsT2,
            3 => AsT3,
            4 => default,
            _ => throw new InvalidOperationException()
        };
        return IsT4;
    }

    private bool Equals(Choice<T0, T1, T2, T3, T4> other) =>
        Index == other.Index &&
        Index switch
        {
            0 => Equals(_value0, other._value0),
            1 => Equals(_value1, other._value1),
            2 => Equals(_value2, other._value2),
            3 => Equals(_value3, other._value3),
            4 => Equals(_value4, other._value4),
            _ => false
        };

    public override bool Equals(object? obj)
    {
        return obj switch
        {
            null => false,
            _ => obj is Choice<T0, T1, T2, T3, T4> o && Equals(o)
        };
    }

    public override string ToString() =>
        Index switch {
            0 => _value0.FormatValue(),
            1 => _value1.FormatValue(),
            2 => _value2.FormatValue(),
            3 => _value3.FormatValue(),
            4 => _value4.FormatValue(),
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
                4 => _value4?.GetHashCode(),
                _ => 0
            } ?? 0;
            return (hashCode*397) ^ Index;
        }
    }
}