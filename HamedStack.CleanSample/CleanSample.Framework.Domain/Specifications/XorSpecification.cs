﻿using System.Linq.Expressions;

namespace CleanSample.Framework.Domain.Specifications;

/// <summary>
/// Represents the logical XOR (Exclusive Or) combination of two specifications.
/// </summary>
/// <typeparam name="T">The type of objects to be queried using the specification.</typeparam>
public class XorSpecification<T> : Specification<T>
{
    private readonly ISpecification<T> _left;
    private readonly ISpecification<T> _right;

    /// <summary>
    /// Initializes a new instance of the <see cref="XorSpecification{T}"/> class.
    /// </summary>
    /// <param name="left">The left side specification.</param>
    /// <param name="right">The right side specification.</param>
    public XorSpecification(ISpecification<T> left, ISpecification<T> right)
    {
        _left = left ?? throw new ArgumentNullException(nameof(left));
        _right = right ?? throw new ArgumentNullException(nameof(right));
    }

    /// <summary>
    /// Returns the combined expression of the left and right specifications using logical XOR.
    /// </summary>
    /// <returns>The combined expression.</returns>
    public override Expression<Func<T, bool>> ToExpression()
    {
        var leftExpression = _left.ToExpression();
        var rightExpression = _right.ToExpression();

        var leftAndNotRight = Expression.AndAlso(
            leftExpression.Body,
            Expression.Not(rightExpression.Body));

        var notLeftAndRight = Expression.AndAlso(
            Expression.Not(leftExpression.Body),
            rightExpression.Body);

        var xorExpression = Expression.OrElse(leftAndNotRight, notLeftAndRight);
        return Expression.Lambda<Func<T, bool>>(xorExpression, leftExpression.Parameters[0]);
    }
}