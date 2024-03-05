using CleanSample.Framework.Domain.ValueObjects;

namespace CleanSample.Domain.ValueObjects;

public class Title : SingleValueObject<string>
{
    private Title()
    {
    }

    public Title(string value) : base(value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Title cannot be empty or null.", nameof(value));
        Value = value.Trim();
    }

    public override string ToString() => Value;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}