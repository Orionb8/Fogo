using System.Linq;

namespace Fogo.Selectors.Extensions {

    public static class IQueryableExtensions {

        public static IQueryable<TResult> Select<TSource, TResult>(
            this IQueryable<TSource> query,
            ISelector selector) {
            return query.Select(selector.Select<TSource, TResult>());
        }
    }
}