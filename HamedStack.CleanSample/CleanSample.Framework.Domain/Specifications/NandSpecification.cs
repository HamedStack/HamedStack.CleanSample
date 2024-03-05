using System.Linq.Expressions;

namespace CleanSample.Framework.Domain.Specifications;

public class NandSpecification<T> : Specification<T>
{
    private readonly ISpecification<T> _left;
    private readonly ISpecification<T> _right;

    public NandSpecification(ISpecification<T> left, ISpecification<T> right)
    {
        _left = left ?? throw new ArgumentNullException(nameof(left));
        _right = right ?? throw new ArgumentNullException(nameof(right));
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        var leftExpression = _left.ToExpression();
        var rightExpression = _right.ToExpression();
        var nandExpression = Expression.Not(Expression.AndAlso(leftExpression.Body, rightExpression.Body));
        return Expression.Lambda<Func<T, bool>>(nandExpression, leftExpression.Parameters[0]);
    }
}