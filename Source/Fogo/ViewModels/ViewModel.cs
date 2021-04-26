using Fogo.Models;
using System;
using System.Linq.Expressions;

namespace Fogo.ViewModels {

    public abstract class ViewModel<TModel> : IComparableModel<TModel> where TModel : class {
        public abstract Expression<Func<TModel, bool>> Comparator { get; }

        public virtual Expression<Func<T, bool>> EqulityComparator<T>(string property, object value) where T : class {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
            Expression expression = Expression.Property(parameter, property);
            expression = Expression.Equal(expression, Expression.Constant(value));
            return Expression.Lambda<Func<T, bool>>(expression, parameter);
        }
    }
}