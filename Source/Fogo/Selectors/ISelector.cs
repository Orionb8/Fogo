using System;
using System.Linq.Expressions;

namespace Fogo.Selectors {

    public interface ISelector {

        Expression<Func<TSource, TResult>> Select<TSource, TResult>();

        ISelector<TSource, TResult> GetSelector<TSource, TResult>();
    }
}