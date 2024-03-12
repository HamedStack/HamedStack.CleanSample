// ReSharper disable UnusedMember.Global
namespace CleanSample.Framework.Domain.Functional.Choices;
public class ChoiceBase<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> : IChoice
{
    private readonly T0? _value0;
    private readonly T1? _value1;
    private readonly T2? _value2;
    private readonly T3? _value3;
    private readonly T4? _value4;
    private readonly T5? _value5;
    private readonly T6? _value6;
    private readonly T7? _value7;
    private readonly T8? _value8;
    private readonly T9? _value9;

    protected ChoiceBase(Choice<T0?, T1?, T2?, T3?, T4?, T5?, T6?, T7?, T8?, T9?> input)
    {
        Index = input.Index;
        switch (Index)
        {
            case 0: _value0 = input.AsT0; break;
            case 1: _value1 = input.AsT1; break;
            case 2: _value2 = input.AsT2; break;
            case 3: _value3 = input.AsT3; break;
            case 4: _value4 = input.AsT4; break;
            case 5: _value5 = input.AsT5; break;
            case 6: _value6 = input.AsT6; break;
            case 7: _value7 = input.AsT7; break;
            case 8: _value8 = input.AsT8; break;
            case 9: _value9 = input.AsT9; break;
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
            4 => _value4,
            5 => _value5,
            6 => _value6,
            7 => _value7,
            8 => _value8,
            9 => _value9,
            _ => throw new InvalidOperationException()
        };

    public int Index { get; }

    public bool IsT0 => Index == 0;
    public bool IsT1 => Index == 1;
    public bool IsT2 => Index == 2;
    public bool IsT3 => Index == 3;
    public bool IsT4 => Index == 4;
    public bool IsT5 => Index == 5;
    public bool IsT6 => Index == 6;
    public bool IsT7 => Index == 7;
    public bool IsT8 => Index == 8;
    public bool IsT9 => Index == 9;

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
    public T5? AsT5 =>
        Index == 5 ?
            _value5 :
            throw new InvalidOperationException($"Cannot return as T5 as result is T{Index}");
    public T6? AsT6 =>
        Index == 6 ?
            _value6 :
            throw new InvalidOperationException($"Cannot return as T6 as result is T{Index}");
    public T7? AsT7 =>
        Index == 7 ?
            _value7 :
            throw new InvalidOperationException($"Cannot return as T7 as result is T{Index}");
    public T8? AsT8 =>
        Index == 8 ?
            _value8 :
            throw new InvalidOperationException($"Cannot return as T8 as result is T{Index}");
    public T9? AsT9 =>
        Index == 9 ?
            _value9 :
            throw new InvalidOperationException($"Cannot return as T9 as result is T{Index}");



    public void Switch(Action<T0?>? f0, Action<T1?>? f1, Action<T2?>? f2, Action<T3?>? f3, Action<T4?>? f4, Action<T5?>? f5, Action<T6?>? f6, Action<T7?>? f7, Action<T8?>? f8, Action<T9?>? f9)
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
            case 5 when f5 != null:
                f5(_value5);
                return;
            case 6 when f6 != null:
                f6(_value6);
                return;
            case 7 when f7 != null:
                f7(_value7);
                return;
            case 8 when f8 != null:
                f8(_value8);
                return;
            case 9 when f9 != null:
                f9(_value9);
                return;
            default:
                throw new InvalidOperationException();
        }
    }

    public TResult? Match<TResult>(Func<T0?, TResult?>? f0, Func<T1?, TResult?>? f1, Func<T2?, TResult?>? f2, Func<T3?, TResult?>? f3, Func<T4?, TResult?>? f4, Func<T5?, TResult?>? f5, Func<T6?, TResult?>? f6, Func<T7?, TResult?>? f7, Func<T8?, TResult?>? f8, Func<T9?, TResult?>? f9)
    {
        return Index switch
        {
            0 when f0 != null => f0(_value0),
            1 when f1 != null => f1(_value1),
            2 when f2 != null => f2(_value2),
            3 when f3 != null => f3(_value3),
            4 when f4 != null => f4(_value4),
            5 when f5 != null => f5(_value5),
            6 when f6 != null => f6(_value6),
            7 when f7 != null => f7(_value7),
            8 when f8 != null => f8(_value8),
            9 when f9 != null => f9(_value9),
            _ => throw new InvalidOperationException()
        };
    }





    public bool TryPickT0(out T0? value, out Choice<T1?, T2?, T3?, T4?, T5?, T6?, T7?, T8?, T9?> remainder)
    {
        value = IsT0 ? AsT0 : default;
        remainder = Index switch
        {
            0 => default,
            1 => AsT1,
            2 => AsT2,
            3 => AsT3,
            4 => AsT4,
            5 => AsT5,
            6 => AsT6,
            7 => AsT7,
            8 => AsT8,
            9 => AsT9,
            _ => throw new InvalidOperationException()
        };
        return IsT0;
    }

    public bool TryPickT1(out T1? value, out Choice<T0?, T2?, T3?, T4?, T5?, T6?, T7?, T8?, T9?> remainder)
    {
        value = IsT1 ? AsT1 : default;
        remainder = Index switch
        {
            0 => AsT0,
            1 => default,
            2 => AsT2,
            3 => AsT3,
            4 => AsT4,
            5 => AsT5,
            6 => AsT6,
            7 => AsT7,
            8 => AsT8,
            9 => AsT9,
            _ => throw new InvalidOperationException()
        };
        return IsT1;
    }

    public bool TryPickT2(out T2? value, out Choice<T0?, T1?, T3?, T4?, T5?, T6?, T7?, T8?, T9?> remainder)
    {
        value = IsT2 ? AsT2 : default;
        remainder = Index switch
        {
            0 => AsT0,
            1 => AsT1,
            2 => default,
            3 => AsT3,
            4 => AsT4,
            5 => AsT5,
            6 => AsT6,
            7 => AsT7,
            8 => AsT8,
            9 => AsT9,
            _ => throw new InvalidOperationException()
        };
        return IsT2;
    }

    public bool TryPickT3(out T3? value, out Choice<T0?, T1?, T2?, T4?, T5?, T6?, T7?, T8?, T9?> remainder)
    {
        value = IsT3 ? AsT3 : default;
        remainder = Index switch
        {
            0 => AsT0,
            1 => AsT1,
            2 => AsT2,
            3 => default,
            4 => AsT4,
            5 => AsT5,
            6 => AsT6,
            7 => AsT7,
            8 => AsT8,
            9 => AsT9,
            _ => throw new InvalidOperationException()
        };
        return IsT3;
    }

    public bool TryPickT4(out T4? value, out Choice<T0?, T1?, T2?, T3?, T5?, T6?, T7?, T8?, T9?> remainder)
    {
        value = IsT4 ? AsT4 : default;
        remainder = Index switch
        {
            0 => AsT0,
            1 => AsT1,
            2 => AsT2,
            3 => AsT3,
            4 => default,
            5 => AsT5,
            6 => AsT6,
            7 => AsT7,
            8 => AsT8,
            9 => AsT9,
            _ => throw new InvalidOperationException()
        };
        return IsT4;
    }

    public bool TryPickT5(out T5? value, out Choice<T0?, T1?, T2?, T3?, T4?, T6?, T7?, T8?, T9?> remainder)
    {
        value = IsT5 ? AsT5 : default;
        remainder = Index switch
        {
            0 => AsT0,
            1 => AsT1,
            2 => AsT2,
            3 => AsT3,
            4 => AsT4,
            5 => default,
            6 => AsT6,
            7 => AsT7,
            8 => AsT8,
            9 => AsT9,
            _ => throw new InvalidOperationException()
        };
        return IsT5;
    }

    public bool TryPickT6(out T6? value, out Choice<T0?, T1?, T2?, T3?, T4?, T5?, T7?, T8?, T9?> remainder)
    {
        value = IsT6 ? AsT6 : default;
        remainder = Index switch
        {
            0 => AsT0,
            1 => AsT1,
            2 => AsT2,
            3 => AsT3,
            4 => AsT4,
            5 => AsT5,
            6 => default,
            7 => AsT7,
            8 => AsT8,
            9 => AsT9,
            _ => throw new InvalidOperationException()
        };
        return IsT6;
    }

    public bool TryPickT7(out T7? value, out Choice<T0?, T1?, T2?, T3?, T4?, T5?, T6?, T8?, T9?> remainder)
    {
        value = IsT7 ? AsT7 : default;
        remainder = Index switch
        {
            0 => AsT0,
            1 => AsT1,
            2 => AsT2,
            3 => AsT3,
            4 => AsT4,
            5 => AsT5,
            6 => AsT6,
            7 => default,
            8 => AsT8,
            9 => AsT9,
            _ => throw new InvalidOperationException()
        };
        return IsT7;
    }

    public bool TryPickT8(out T8? value, out Choice<T0?, T1?, T2?, T3?, T4?, T5?, T6?, T7?, T9?> remainder)
    {
        value = IsT8 ? AsT8 : default;
        remainder = Index switch
        {
            0 => AsT0,
            1 => AsT1,
            2 => AsT2,
            3 => AsT3,
            4 => AsT4,
            5 => AsT5,
            6 => AsT6,
            7 => AsT7,
            8 => default,
            9 => AsT9,
            _ => throw new InvalidOperationException()
        };
        return IsT8;
    }

    public bool TryPickT9(out T9? value, out Choice<T0?, T1?, T2?, T3?, T4?, T5?, T6?, T7?, T8?> remainder)
    {
        value = IsT9 ? AsT9 : default;
        remainder = Index switch
        {
            0 => AsT0,
            1 => AsT1,
            2 => AsT2,
            3 => AsT3,
            4 => AsT4,
            5 => AsT5,
            6 => AsT6,
            7 => AsT7,
            8 => AsT8,
            9 => default,
            _ => throw new InvalidOperationException()
        };
        return IsT9;
    }

    private bool Equals(ChoiceBase<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> other) =>
        Index == other.Index &&
        Index switch
        {
            0 => Equals(_value0, other._value0),
            1 => Equals(_value1, other._value1),
            2 => Equals(_value2, other._value2),
            3 => Equals(_value3, other._value3),
            4 => Equals(_value4, other._value4),
            5 => Equals(_value5, other._value5),
            6 => Equals(_value6, other._value6),
            7 => Equals(_value7, other._value7),
            8 => Equals(_value8, other._value8),
            9 => Equals(_value9, other._value9),
            _ => false
        };

    public override bool Equals(object? obj)
    {
        switch (obj)
        {
            case null:
                return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        return obj is ChoiceBase<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> o && Equals(o);
    }

    public override string ToString() =>
        Index switch
        {
            0 => _value0.FormatValue(),
            1 => _value1.FormatValue(),
            2 => _value2.FormatValue(),
            3 => _value3.FormatValue(),
            4 => _value4.FormatValue(),
            5 => _value5.FormatValue(),
            6 => _value6.FormatValue(),
            7 => _value7.FormatValue(),
            8 => _value8.FormatValue(),
            9 => _value9.FormatValue(),
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
                5 => _value5?.GetHashCode(),
                6 => _value6?.GetHashCode(),
                7 => _value7?.GetHashCode(),
                8 => _value8?.GetHashCode(),
                9 => _value9?.GetHashCode(),
                _ => 0
            } ?? 0;
            return (hashCode * 397) ^ Index;
        }
    }
}