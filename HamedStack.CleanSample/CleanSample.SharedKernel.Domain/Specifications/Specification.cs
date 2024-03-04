// ReSharper disable IdentifierTypo
// ReSharper disable UnusedMember.Global

using System.Linq.Expressions;

namespace CleanSample.SharedKernel.Domain.Specifications;

/// <summary>
/// Provides the base functionality for creating and combining query specifications.
/// </summary>
/// <typeparam name="T">The type of objects to be queried using the specification.</typeparam>
public abstract class Specification<T> : ISpecification<T>
{
    /// <summary>
    /// Converts this specification into an expression.
    /// </summary>
    /// <returns>An expression representing the query specification.</returns>
    public abstract Expression<Func<T, bool>> ToExpression();

    /// <summary>
    /// Determines if the given entity satisfies the current specification.
    /// </summary>
    /// <param name="entity">The entity to evaluate.</param>
    /// <returns>true if the entity satisfies the specification; otherwise, false.</returns>
    public bool IsSatisfiedBy(T entity)
    {
        var predicate = ToExpression().Compile();
        return predicate(entity);
    }

    /// <summary>
    /// Combines the current specification with the provided one using logical AND.
    /// </summary>
    /// <param name="specification">The specification to combine with.</param>
    /// <returns>A new specification.</returns>
    public Specification<T> And(ISpecification<T> specification)
    {
        return new AndSpecification<T>(this, specification);
    }

    /// <summary>
    /// Combines the current specification with the provided one using logical OR.
    /// </summary>
    /// <param name="specification">The specification to combine with.</param>
    /// <returns>A new specification.</returns>
    public Specification<T> Or(ISpecification<T> specification)
    {
        return new OrSpecification<T>(this, specification);
    }

    /// <summary>
    /// Negates the current specification.
    /// </summary>
    /// <returns>A new negated specification.</returns>
    public Specification<T> Not()
    {
        return new NotSpecification<T>(this);
    }

    /// <summary>
    /// Combines the current specification with the provided one using logical XOR (Exclusive OR).
    /// </summary>
    /// <param name="specification">The specification to combine with.</param>
    /// <returns>A new specification.</returns>
    /// <remarks>
    /// The XOR operation follows this truth table:
    /// <code>
    /// | Current Spec | Provided Spec | Result |
    /// |--------------|---------------|--------|
    /// | True         | True          | False  |
    /// | True         | False         | True   |
    /// | False        | True          | True   |
    /// | False        | False         | False  |
    /// </code>
    /// This method allows for the construction of complex conditional logic by combining specifications in a way that the result is only true if the specifications differ in their satisfaction of the criteria.
    /// </remarks>
    public Specification<T> Xor(ISpecification<T> specification)
    {
        return new XorSpecification<T>(this, specification);
    }
    public Specification<T> XNor(ISpecification<T> specification)
    {
        return new XNorSpecification<T>(this, specification);
    }
    public Specification<T> Nand(ISpecification<T> specification)
    {
        return new NandSpecification<T>(this, specification);
    }
    public Specification<T> Nor(ISpecification<T> specification)
    {
        return new NorSpecification<T>(this, specification);
    }
    public Specification<T> True()
    {
        return new TrueSpecification<T>();
    }
    public Specification<T> False()
    {
        return new FalseSpecification<T>();
    }
}