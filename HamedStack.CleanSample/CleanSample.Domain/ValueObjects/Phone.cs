using CleanSample.Framework.Domain.ValueObjects;
using Light.GuardClauses;

namespace CleanSample.Domain.ValueObjects;

public class Phone : ValueObject
{
    public string Value { get; } = null!;

    private Phone(){}
    public Phone(string value)
    {
        value.MustNotBeNull().MustNotBeNullOrEmpty().MustNotBeNullOrWhiteSpace();

        Value = value;
    }

    public override string ToString() => Value;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}