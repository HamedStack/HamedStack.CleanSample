using CleanSample.SharedKernel.Domain.ValueObjects;
using Light.GuardClauses;

namespace CleanSample.Domain.ValueObjects;

public class Email : ValueObject
{
    public string Value { get; } = null!;

    private Email()
    {
    }

    public Email(string value)
    {
        value.MustNotBeNull().MustBeEmailAddress();

        Value = value;
    }
    public override string ToString() => Value;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}