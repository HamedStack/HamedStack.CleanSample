// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

using MediatR;

namespace CleanSample.Framework.Domain.Functional.Extensions;

/// <summary>
///     Contains extension methods for piping operations on objects.
/// </summary>
public static class PipeExtensions
{
    /// <summary>
    ///     Pipes the given object into the provided function and returns the result.
    /// </summary>
    /// <typeparam name="T">The type of the object being piped.</typeparam>
    /// <typeparam name="TU">The return type of the function.</typeparam>
    /// <param name="obj">The object to pipe.</param>
    /// <param name="f">The function to apply on the piped object.</param>
    /// <returns>The result of applying the function on the object.</returns>
    public static TU Pipe<T, TU>(this T obj, Func<T, TU> f)
    {
        return f(obj);
    }

    /// <summary>
    ///     Pipes the given object into the provided action and returns the object.
    /// </summary>
    /// <typeparam name="T">The type of the object being piped.</typeparam>
    /// <param name="obj">The object to pipe.</param>
    /// <param name="f">The action to apply on the piped object.</param>
    /// <returns>The original object after applying the action.</returns>
    public static T Pipe<T>(this T obj, Action<T> f)
    {
        f(obj);
        return obj;
    }

    /// <summary>
    ///     Asynchronously pipes the given object into the provided function and returns the result.
    /// </summary>
    /// <typeparam name="T">The type of the object being piped.</typeparam>
    /// <typeparam name="TU">The return type of the function.</typeparam>
    /// <param name="obj">The object to pipe.</param>
    /// <param name="f">The function to apply on the piped object.</param>
    /// <returns>The result of applying the function on the object.</returns>
    public static async Task<TU> PipeAsync<T, TU>(this T obj, Func<T, Task<TU>> f)
    {
        return await f(obj);
    }

    /// <summary>
    ///     Asynchronously pipes the given object into the provided function and returns the object.
    /// </summary>
    /// <typeparam name="T">The type of the object being piped.</typeparam>
    /// <param name="obj">The object to pipe.</param>
    /// <param name="f">The function to apply on the piped object.</param>
    /// <returns>The original object after applying the function.</returns>
    public static async Task<T> PipeAsync<T>(this T obj, Func<T, Task> f)
    {
        await f(obj);
        return obj;
    }

    /// <summary>
    ///     Asynchronously pipes the given object into the provided function without returning a result.
    /// </summary>
    /// <typeparam name="T">The type of the object being piped.</typeparam>
    /// <param name="obj">The object to pipe.</param>
    /// <param name="f">The function to apply on the piped object.</param>
    public static async Task PipeTaskAsync<T>(this T obj, Func<T, Task> f)
    {
        await f(obj);
    }

    /// <summary>
    ///     Pipes the given object into the provided action and returns a unit type.
    /// </summary>
    /// <typeparam name="T">The type of the object being piped.</typeparam>
    /// <param name="obj">The object to pipe.</param>
    /// <param name="f">The action to apply on the piped object.</param>
    /// <returns>A unit type.</returns>
    public static Unit PipeUnit<T>(this T obj, Action<T> f)
    {
        f(obj);
        return Unit.Value;
    }

    /// <summary>
    ///     Asynchronously pipes the given object into the provided function and returns a unit type.
    /// </summary>
    /// <typeparam name="T">The type of the object being piped.</typeparam>
    /// <param name="obj">The object to pipe.</param>
    /// <param name="f">The function to apply on the piped object.</param>
    /// <returns>A unit type.</returns>
    public static async Task<Unit> PipeUnitAsync<T>(this T obj, Func<T, Task> f)
    {
        await f(obj);
        return Unit.Value;
    }

    /// <summary>
    ///     Pipes the given object into the provided action without returning a result.
    /// </summary>
    /// <typeparam name="T">The type of the object being piped.</typeparam>
    /// <param name="obj">The object to pipe.</param>
    /// <param name="f">The action to apply on the piped object.</param>
    public static void PipeVoid<T>(this T obj, Action<T> f)
    {
        f(obj);
    }
}