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
