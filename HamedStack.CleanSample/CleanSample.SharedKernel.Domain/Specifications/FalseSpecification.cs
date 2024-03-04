using System.Linq.Expressions;

namespace CleanSample.SharedKernel.Domain.Specifications;

public class FalseSpecification<T> : Specification<T>
{
    public override Expression<Func<T, bool>> ToExpression()
    {
        return entity => false;
    }
}