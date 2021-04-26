using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Fogo.Requests.Extensions {

    public static class RequestExtensions {

        public static Result<TModel> ToResult<TModel>(
            this Request request,
            IQueryable<TModel> query) {
            int total = query.Count();
            if (total == 0) {
                return new Result<TModel>(request);
            }
            return new Result<TModel>(request, total, query
                .Skip(request.Skip)
                .Take(request.Take)
                .ToList());
        }

        public static async Task<Result<TModel>> ToResultAsync<TModel>(
            this Request request,
            IQueryable<TModel> query,
            CancellationToken cancellationToken = default) {
            int total = await query.CountAsync(cancellationToken);
            if (total == 0) {
                return new Result<TModel>(request);
            }
            return new Result<TModel>(request, total, await query
                .Skip(request.Skip)
                .Take(request.Take)
                .ToListAsync(cancellationToken));
        }
    }
}