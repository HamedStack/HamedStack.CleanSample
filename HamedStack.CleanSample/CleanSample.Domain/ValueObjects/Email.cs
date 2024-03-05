using CleanSample.SharedKernel.Domain.ValueObjects;
using Light.GuardClauses;

namespace CleanSample.Domain.ValueObjects;

public class Email : SingleValueObject<string>
{
    private Email()
    {
    }

    public Email(string value)
    {
        Value = value.MustNotBeNull().MustBeEmailAddress();
    }
    public override string ToString() => Value;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}