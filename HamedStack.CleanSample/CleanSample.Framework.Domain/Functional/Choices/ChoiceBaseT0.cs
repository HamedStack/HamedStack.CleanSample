// ReSharper disable UnusedMember.Global
namespace CleanSample.Framework.Domain.Functional.Choices;

public class ChoiceBase<T0> : IChoice
{
    private readonly T0? _value0;

    protected ChoiceBase(Choice<T0?> input)
    {
        Index = input.Index;
        _value0 = Index switch
        {
            0 => input.AsT0,
            _ => throw new InvalidOperationException()
        };
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


    private bool Equals(ChoiceBase<T0> other) =>
        Index == other.Index &&
        Index switch
        {
            0 => Equals(_value0, other._value0),
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

        return obj is ChoiceBase<T0> o && Equals(o);
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