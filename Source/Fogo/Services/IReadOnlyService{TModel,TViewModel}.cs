using Fogo.Models;
using Fogo.Requests;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fogo.Services {

    public interface IReadOnlyService<TModel, TViewModel> :
        IAsyncDisposable,
        IDisposable
        where TModel : class, new()
        where TViewModel : class, IComparableModel<TModel>, new() {

        Task<PageResult<TViewModel>> ReadAsync(PageRequest request, CancellationToken cancellationToken = default);

        Task<Result<TViewModel>> ReadAsync(Request request, CancellationToken cancellationToken = default);

        Task<TViewModel> ReadAsync(TViewModel viewModel, CancellationToken cancellationToken = default);
    }
}