namespace CleanSample.Framework.Domain.ValueObjects;

[Serializable]
public class SingleValueObject<T> : ValueObject
{
    protected SingleValueObject()
    {

    }
    public SingleValueObject(T value)
    {
        Value = value;
    }

    public T Value { get; set; } = default!;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}