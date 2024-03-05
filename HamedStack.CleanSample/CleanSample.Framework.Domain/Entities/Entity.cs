namespace CleanSample.Framework.Domain.Entities;

/// <summary>
/// Represents the base entity for domain entities.
/// </summary>
/// <typeparam name="TId">The type of the entity identifier.</typeparam>
public abstract class Entity<TId> : IId<TId>
    where TId : notnull
{
    private int _requestedHashCode = int.MinValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{T}"/> class with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier for the entity.</param>
    protected Entity(TId id)
    {
        Id = id;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{T}"/> class without setting an identifier.
    /// </summary>
    protected Entity()
    {
        Id = default!;
    }

    /// <summary>
    /// Gets or sets the identifier for the entity.
    /// </summary>
    public TId Id { get; }

    /// <summary>
    /// Determines whether two entities are not equal.
    /// </summary>
    /// <param name="left">The first entity to compare.</param>
    /// <param name="right">The second entity to compare.</param>
    /// <returns>true if the entities are not equal; otherwise, false.</returns>
    public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Determines whether two entities are equal.
    /// </summary>
    /// <param name="left">The first entity to compare.</param>
    /// <param name="right">The second entity to compare.</param>
    /// <returns>true if the entities are equal; otherwise, false.</returns>
    public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
    {
        return left?.Equals(right) ?? right is null;
    }

    /// <summary>
    /// Determines whether the current entity is equal to another entity.
    /// </summary>
    /// <param name="obj">The object to compare with the current entity.</param>
    /// <returns>true if the current entity is equal to the other entity; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TId> entityItem)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (GetType() != obj.GetType())
            return false;

        if (entityItem.IsNotPersisted() || IsNotPersisted())
            return false;

        return Id.Equals(entityItem.Id);
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current entity.</returns>
    public override int GetHashCode()
    {
        // If the entity is not persisted, just use the base implementation.
        if (IsNotPersisted())
            // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
            return base.GetHashCode();

        // Use cached hash code if available.
        // ReSharper disable once NonReadonlyMemberInGetHashCode
        if (_requestedHashCode != int.MinValue)
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return _requestedHashCode;

        // Compute and cache the hash code using HashCode struct.
        // ReSharper disable once NonReadonlyMemberInGetHashCode
        _requestedHashCode = HashCode.Combine(Id);

        // ReSharper disable once NonReadonlyMemberInGetHashCode
        return _requestedHashCode;
    }

    /// <summary>
    /// Determines whether the entity is not persisted yet by comparing its identifier to the
    /// default value of its type.
    /// </summary>
    /// <returns>
    /// true if the entity's identifier matches the default value of its type, indicating it is not
    /// persisted; otherwise, false.
    /// </returns>
    public bool IsNotPersisted()
    {
        return Id.Equals(default(TId));
    }
}