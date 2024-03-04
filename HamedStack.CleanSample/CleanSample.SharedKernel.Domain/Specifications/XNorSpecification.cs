using System.Linq.Expressions;

namespace CleanSample.SharedKernel.Domain.Specifications;

public class XNorSpecification<T> : Specification<T>
{
    private readonly ISpecification<T> _left;
    private readonly ISpecification<T> _right;

    public XNorSpecification(ISpecification<T> left, ISpecification<T> right)
    {
        _left = left ?? throw new ArgumentNullException(nameof(left));
        _right = right ?? throw new ArgumentNullException(nameof(right));
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        var leftExpression = _left.ToExpression();
        var rightExpression = _right.ToExpression();

        var leftAndRight = Expression.AndAlso(leftExpression.Body, rightExpression.Body);
        var notLeftAndNotRight = Expression.AndAlso(Expression.Not(leftExpression.Body), Expression.Not(rightExpression.Body));

        var xNorExpression = Expression.OrElse(leftAndRight, notLeftAndNotRight);

        return Expression.Lambda<Func<T, bool>>(xNorExpression, leftExpression.Parameters[0]);
    }
}