﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cash.Domain.Compare;
public class PredicateExpressionComparer<TSource> : IComparer<Expression<Func<TSource, bool>>>
{
    public int Compare(Expression<Func<TSource, bool>>? x, Expression<Func<TSource, bool>>? y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (ReferenceEquals(null, y)) return 1;
        if (ReferenceEquals(null, x)) return -1;

        return string.Compare(x.ToString(), y.ToString(), StringComparison.Ordinal);
    }
}
