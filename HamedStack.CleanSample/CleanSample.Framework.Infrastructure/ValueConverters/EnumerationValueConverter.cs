using CleanSample.Framework.Domain.Enumerations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CleanSample.Framework.Infrastructure.ValueConverters;

public class EnumerationValueConverter<TEnumeration> : ValueConverter<TEnumeration, long>
    where TEnumeration : Enumeration<TEnumeration>
{
    public EnumerationValueConverter() : base(
        v => v.Value,
        v => Enumeration<TEnumeration>.FromValue(v))
    {
    }
}

public class EnumerationNameValueConverter<TEnumeration> : ValueConverter<TEnumeration, string>
    where TEnumeration : Enumeration<TEnumeration>
{
    public EnumerationNameValueConverter() : base(
        v => v.Name,
        v => Enumeration<TEnumeration>.FromName(v))
    {
    }
}