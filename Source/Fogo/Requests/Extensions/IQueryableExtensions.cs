using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Fogo.Requests.Extensions {

    public static class IQueryableExtensions {

        public static Result<TModel> ToResult<TModel>(
            this IQueryable<TModel> query,
            Request request) {
            return request.ToResult(query);
        }

        public static PageResult<TModel> ToPageResult<TModel>(
            this IQueryable<TModel> query,
            PageRequest request) {
            return request.ToPageResult(query);
        }

        public static Task<Result<TModel>> ToResultAsync<TModel>(
            this IQueryable<TModel> query,
            Request request,
            CancellationToken cancellationToken = default) {
            return request.ToResultAsync(query, cancellationToken);
        }

        public static Task<PageResult<TModel>> ToPageResultAsync<TModel>(
            this IQueryable<TModel> query,
            PageRequest request,
            CancellationToken cancellationToken = default) {
            return request.ToPageResultAsync(query, cancellationToken);
        }
    }
}