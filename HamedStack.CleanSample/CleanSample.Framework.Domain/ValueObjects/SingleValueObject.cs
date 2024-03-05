﻿namespace CleanSample.Framework.Domain.ValueObjects;

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

    public static implicit operator T(SingleValueObject<T> valueObject)
    {
        return valueObject.Value;
    }

    public static implicit operator SingleValueObject<T>(T value)
    {
        return new SingleValueObject<T>(value);
    }

    public T Value { get; set; } = default!;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}