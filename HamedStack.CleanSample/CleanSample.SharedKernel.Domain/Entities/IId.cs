namespace CleanSample.SharedKernel.Domain.Entities;

/// <summary>
/// Represents a contract for objects that must have an identifier.
/// </summary>
/// <typeparam name="TId">The type of the identifier.</typeparam>
public interface IId<out TId> where TId : notnull
{
    /// <summary>
    /// Gets the unique identifier for the object.
    /// </summary>
    /// <value>The unique identifier.</value>
    TId Id { get; }
}