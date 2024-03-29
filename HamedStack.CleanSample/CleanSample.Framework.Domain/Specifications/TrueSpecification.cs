﻿using System.Linq.Expressions;

namespace CleanSample.Framework.Domain.Specifications;

public class TrueSpecification<T> : Specification<T>
{
    public override Expression<Func<T, bool>> ToExpression()
    {
        return entity => true;
    }
}