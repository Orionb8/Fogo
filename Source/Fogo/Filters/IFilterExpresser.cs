using System;
using System.Linq.Expressions;

namespace Fogo.Filters {

    public interface IFilterExpresser {

        Expression<Func<TSource, bool>> Express<TSource>(Filter filter) where TSource : class;
    }
}