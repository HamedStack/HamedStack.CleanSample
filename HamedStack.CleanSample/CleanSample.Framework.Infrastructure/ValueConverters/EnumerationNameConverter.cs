using CleanSample.Framework.Domain.Enumerations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CleanSample.Framework.Infrastructure.ValueConverters;

public class EnumerationNameConverter<TEnumeration> : ValueConverter<TEnumeration, string>
    where TEnumeration : Enumeration<TEnumeration>
{
    public EnumerationNameConverter() : base(
        v => v.Name,
        v => Enumeration<TEnumeration>.FromName(v))
    {
    }
}