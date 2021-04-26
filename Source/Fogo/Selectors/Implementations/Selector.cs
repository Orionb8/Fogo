using System;
using System.Linq.Expressions;

namespace Fogo.Selectors.Implementations {

    public class Selector : ISelector {
        protected readonly IServiceProvider _provider;

        public Selector(IServiceProvider provider) {
            _provider = provider;
        }

        public Expression<Func<TSource, TResult>> Select<TSource, TResult>() {
            return GetSelector<TSource, TResult>().Select;
        }

        public ISelector<TSource, TResult> GetSelector<TSource, TResult>() {
            return _provider.GetService(typeof(ISelector<TSource, TResult>)) as ISelector<TSource, TResult>;
        }
    }
}