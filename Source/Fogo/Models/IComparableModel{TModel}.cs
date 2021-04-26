using System;
using System.Linq.Expressions;

namespace Fogo.Models {

    public interface IComparableModel<TModel> where TModel : class {
        Expression<Func<TModel, bool>> Comparator { get; }
    }
}