using CleanSample.SharedKernel.Domain.ValueObjects;

namespace CleanSample.Domain.ValueObjects;

public class Address(string street, string city, string state, string country, string postalCode)
    : ValueObject
{
    public string Street { get; } = street;
    public string City { get; } = city;
    public string State { get; } = state;
    public string Country { get; } = country;
    public string PostalCode { get; } = postalCode;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Street.Trim().ToLower();
        yield return City.Trim().ToLower();
        yield return State.Trim().ToLower();
        yield return Country.Trim().ToLower();
        yield return PostalCode.Trim().ToLower();
    }
}