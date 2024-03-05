using CleanSample.Framework.Domain.ValueObjects;
using Light.GuardClauses;

namespace CleanSample.Domain.ValueObjects;

public class FullName : ValueObject
{
    public string FirstName { get; } = null!;
    public string LastName { get; } = null!;

    private FullName()
    {

    }
    public FullName(string firstName, string lastName)
    {
        firstName.MustNotBeNullOrWhiteSpace();
        lastName.MustNotBeNullOrWhiteSpace();

        FirstName = firstName;
        LastName = lastName;
    }

    public override string? ToString() => $"{FirstName} {LastName}";
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
    }

    public static FullName FromSingleString(string name)
    {
        var parts = name.Split(new[] { ' ' }, 2);
        var firstName = parts.Length > 0 ? parts[0] : string.Empty;
        var lastName = parts.Length > 1 ? parts[1] : string.Empty;
        return new FullName(firstName, lastName);
    }
}
