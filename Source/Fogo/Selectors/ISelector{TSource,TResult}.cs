using System;
using System.Linq.Expressions;

namespace Fogo.Selectors {

    public interface ISelector<TSource, TResult> {
        Expression<Func<TSource, TResult>> Select { get; }
    }
}