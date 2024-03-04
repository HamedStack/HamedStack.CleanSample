// ReSharper disable UnusedMember.Global

using System.Linq.Expressions;

namespace CleanSample.SharedKernel.Domain.Specifications;

/// <summary>
/// Provides an interface for creating specifications for querying objects of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of objects to be queried using the specification.</typeparam>
public interface ISpecification<T>
{
    /// <summary>
    /// Transforms the specification into an expression that can be used in LINQ queries.
    /// </summary>
    /// <returns>An expression that represents the specification.</returns>
    Expression<Func<T, bool>> ToExpression();

    /// <summary>
    /// Checks if the given entity satisfies the specification.
    /// </summary>
    /// <param name="entity">The entity to be checked.</param>
    /// <returns>true if the entity satisfies the specification; otherwise, false.</returns>
    bool IsSatisfiedBy(T entity);
}