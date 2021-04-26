using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Fogo.Requests.Extensions {

    public static class PageRequestExtensions {

        public static PageResult<TModel> ToPageResult<TModel>(
            this PageRequest request,
            IQueryable<TModel> query) {
            int total = query.Count();
            if (total == 0) {
                return new PageResult<TModel>(request);
            }
            return new PageResult<TModel>(request, total, query
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size)
                .ToList());
        }

        public static async Task<PageResult<TModel>> ToPageResultAsync<TModel>(
            this PageRequest request,
            IQueryable<TModel> query,
            CancellationToken cancellationToken = default) {
            int total = await query.CountAsync(cancellationToken);
            if (total == 0) {
                return new PageResult<TModel>(request);
            }
            return new PageResult<TModel>(request, total, await query
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size)
                .ToListAsync(cancellationToken));
        }
    }
}