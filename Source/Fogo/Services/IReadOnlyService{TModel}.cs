using Fogo.Models;
using Fogo.Requests;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fogo.Services {

    public interface IReadOnlyService<TModel> :
        IAsyncDisposable,
        IDisposable
        where TModel : class, IComparableModel<TModel>, new() {

        Task<PageResult<TModel>> ReadAsync(PageRequest request, CancellationToken cancellationToken = default);

        Task<Result<TModel>> ReadAsync(Request request, CancellationToken cancellationToken = default);

        Task<TModel> ReadAsync(TModel model, CancellationToken cancellationToken = default);
    }
}