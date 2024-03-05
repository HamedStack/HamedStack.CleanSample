using CleanSample.Framework.Domain.ValueObjects;

namespace CleanSample.Domain.ValueObjects;

public class Address : ValueObject
{
    private Address()
    {
    }

    public Address(string street, string city, string state, string country, string postalCode)
    {
        Street = street;
        City = city;
        State = state;
        Country = country;
        PostalCode = postalCode;
    }

    public string Street { get; } = null!;
    public string City { get; } = null!;
    public string State { get; } = null!;
    public string Country { get; } = null!;
    public string PostalCode { get; } = null!;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Street.Trim().ToLower();
        yield return City.Trim().ToLower();
        yield return State.Trim().ToLower();
        yield return Country.Trim().ToLower();
        yield return PostalCode.Trim().ToLower();
    }
}