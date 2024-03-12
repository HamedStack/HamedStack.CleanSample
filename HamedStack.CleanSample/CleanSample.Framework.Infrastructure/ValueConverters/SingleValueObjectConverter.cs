using CleanSample.Framework.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CleanSample.Framework.Infrastructure.ValueConverters;

public class SingleValueObjectConverter<TSingleValueObject, TValue> : ValueConverter<TSingleValueObject, TValue>
    where TSingleValueObject : SingleValueObject<TValue>, new()
{
    public SingleValueObjectConverter() : base(
        vo => vo.Value,
        value => new TSingleValueObject { Value = value })
    {
    }
}