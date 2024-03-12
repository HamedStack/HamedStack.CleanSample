// ReSharper disable UnusedMember.Global
namespace CleanSample.Framework.Domain.Functional.Choices;

internal static class ChoiceExtensions
{
    internal static string FormatValue<T>(this T value) => $"{typeof(T).FullName}: {value?.ToString()}";
    internal static string? FormatValue<T>(this object @this, object @base, T value) =>
        ReferenceEquals(@this, value) ?
            @base.ToString() :
            $"{typeof(T).FullName}: {value?.ToString()}";
}