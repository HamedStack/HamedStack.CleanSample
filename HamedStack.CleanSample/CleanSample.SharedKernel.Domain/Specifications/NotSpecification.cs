using System.Linq.Expressions;

namespace CleanSample.SharedKernel.Domain.Specifications;

/// <summary>
/// Represents the logical negation of a specification.
/// </summary>
/// <typeparam name="T">The type of objects to be queried using the specification.</typeparam>
public class NotSpecification<T> : Specification<T>
{
    private readonly ISpecification<T> _specification;

    /// <summary>
    /// Initializes a new instance of the <see cref="NotSpecification{T}"/> class.
    /// </summary>
    /// <param name="specification">The specification to negate.</param>
    public NotSpecification(ISpecification<T> specification)
    {
        _specification = specification ?? throw new ArgumentNullException(nameof(specification));
    }

    /// <summary>
    /// Returns the negated expression of the specification.
    /// </summary>
    /// <returns>The negated expression.</returns>
    public override Expression<Func<T, bool>> ToExpression()
    {
        var expression = _specification.ToExpression();

        var notExpression = Expression.Not(expression.Body);

        return Expression.Lambda<Func<T, bool>>(notExpression, expression.Parameters[0]);
    }
}