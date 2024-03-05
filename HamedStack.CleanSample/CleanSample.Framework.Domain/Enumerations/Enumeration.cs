// ReSharper disable StaticMemberInGenericType

using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace CleanSample.Framework.Domain.Enumerations;

public abstract class Enumeration<T> : IComparable
    where T : Enumeration<T>
{
    private static readonly HashSet<string> ExistingNames = new();
    private static readonly HashSet<long> ExistingValues = new();
    private static readonly ConcurrentDictionary<Type, List<Enumeration<T>>> Cache = new();

    public long Value { get; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    protected Enumeration()
    {
    }

    protected Enumeration(string name, int value, string? description = "")
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name), "Name cannot be null or empty.");

        if (value < 0)
            throw new ArgumentException("Value cannot be negative.", nameof(value));

        if (!ExistingNames.Add(name))
            throw new InvalidOperationException($"The name '{name}' is already used.");

        if (!ExistingValues.Add(value))
            throw new InvalidOperationException($"The value '{value}' is already used.");

        (Name, Value, Description) = (name, value, description ?? string.Empty);
    }

    protected Enumeration(string name, long value, string? description = "")
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name), "Name cannot be null or empty.");

        if (value < 0)
            throw new ArgumentException("Value cannot be negative.", nameof(value));

        if (!ExistingNames.Add(name))
            throw new InvalidOperationException($"The name '{name}' is already used.");

        if (!ExistingValues.Add(value))
            throw new InvalidOperationException($"The value '{value}' is already used.");

        (Value, Name, Description) = (value, name, description ?? string.Empty);
    }

    public static implicit operator int(Enumeration<T> enumeration)
    {
        return Convert.ToInt32(enumeration.Value);
    }
    public static implicit operator long(Enumeration<T> enumeration)
    {
        return enumeration.Value;
    }
    public static implicit operator string(Enumeration<T> enumeration)
    {
        return enumeration.Name;
    }
    public static implicit operator Enumeration<T>(string name)
    {
        return FromName(name);
    }
    public static implicit operator Enumeration<T>(int value)
    {
        return FromValue(value);
    }
    public static implicit operator Enumeration<T>(long value)
    {
        return FromValue(value);
    }
    public override string ToString() => Name;

    public static IEnumerable<T> GetAll(bool useCache = true)
    {
        if (useCache)
            if (Cache.TryGetValue(typeof(T), out var values))
                return values.Cast<T>();

        var items = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Where(f => f.FieldType == typeof(T))
            .Select(f => f.GetValue(null))
            .Cast<T>()
            .ToList();

        if (useCache)
            Cache[typeof(T)] = items.OfType<Enumeration<T>>().ToList();

        return items;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Enumeration<T> otherValue)
        {
            return false;
        }

        var typeMatches = GetType() == obj.GetType();
        var valueMatches = Value.Equals(otherValue.Value);

        return typeMatches && valueMatches;
    }

    public override int GetHashCode() => Value.GetHashCode();

    public static long AbsoluteDifference(Enumeration<T> firstValue, Enumeration<T> secondValue)
    {
        var absoluteDifference = Math.Abs(firstValue.Value - secondValue.Value);
        return absoluteDifference;
    }

    private static T Parse(Func<T, bool> predicate)
    {
        var matchingItem = GetAll().FirstOrDefault(predicate);
        return matchingItem ?? throw new InvalidOperationException($"No matching enumeration value found in {typeof(T)}");
    }

    public static T FromValue(int value)
    {
        return Parse(item => item.Value == value);
    }

    public static T FromValue(long value)
    {
        return Parse(item => item.Value == value);
    }

    public static bool HasDescription(int value)
    {
        var val = Parse(item => item.Value == value);
        return !string.IsNullOrWhiteSpace(val.Description);
    }

    public static bool HasDescription(long value)
    {
        var val = Parse(item => item.Value == value);
        return !string.IsNullOrWhiteSpace(val.Description);
    }

    public static T FromName(string name)
    {
        return Parse(item => item.Name == name);
    }

    public static bool HasDescription(string name)
    {
        var val = Parse(item => item.Name == name);
        return !string.IsNullOrWhiteSpace(val.Description);
    }

    public static IEnumerable<Enumeration<T>> Values => GetAll();

    public static bool TryFromValue(int value, [NotNullWhen(true)] out T? result)
    {
        result = GetAll().FirstOrDefault(item => item.Value == value);
        return result is not null;
    }
    public static bool TryFromValue(long value, [NotNullWhen(true)] out T? result)
    {
        result = GetAll().FirstOrDefault(item => item.Value == value);
        return result is not null;
    }
    public static bool TryFromName(string name, [NotNullWhen(true)] out T? result)
    {
        result = GetAll().FirstOrDefault(item => item.Name == name);
        return result is not null;
    }

    public int CompareTo(object? other)
    {
        return other switch
        {
            null => 1,
            _ => other is not Enumeration<T> otherEnumeration
                ? throw new ArgumentException("Comparing Enumeration type with different type is not supported.")
                : Value.CompareTo(otherEnumeration.Value)
        };
    }

    public int CompareTo(Enumeration<T>? other)
    {
        return other is null ? 1 : Value.CompareTo(other.Value);
    }

    public static bool operator <(Enumeration<T>? left, Enumeration<T>? right)
        => left is null ? right is not null : left.Value < right?.Value;

    public static bool operator >(Enumeration<T>? left, Enumeration<T>? right)
        => left is not null && (right is null || left.Value > right.Value);

    public static bool operator <=(Enumeration<T>? left, Enumeration<T>? right)
        => left is null || left.Value <= right?.Value;

    public static bool operator >=(Enumeration<T>? left, Enumeration<T>? right)
        => right is null || left is not null && left.Value >= right.Value;

    public static bool operator ==(Enumeration<T>? left, Enumeration<T>? right)
        => left?.Equals(right) ?? right is null;

    public static bool operator !=(Enumeration<T>? left, Enumeration<T>? right)
        => !(left == right);
}