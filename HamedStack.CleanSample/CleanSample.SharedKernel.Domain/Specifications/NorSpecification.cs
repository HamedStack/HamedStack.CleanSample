using System.Linq.Expressions;

namespace CleanSample.SharedKernel.Domain.Specifications;

public class NorSpecification<T> : Specification<T>
{
    private readonly ISpecification<T> _left;
    private readonly ISpecification<T> _right;

    public NorSpecification(ISpecification<T> left, ISpecification<T> right)
    {
        _left = left ?? throw new ArgumentNullException(nameof(left));
        _right = right ?? throw new ArgumentNullException(nameof(right));
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        var leftExpression = _left.ToExpression();
        var rightExpression = _right.ToExpression();
        var norExpression = Expression.AndAlso(Expression.Not(leftExpression.Body), Expression.Not(rightExpression.Body));
        return Expression.Lambda<Func<T, bool>>(norExpression, leftExpression.Parameters[0]);
    }
}