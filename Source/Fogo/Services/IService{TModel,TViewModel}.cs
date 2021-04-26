using Fogo.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Fogo.Services {

    public interface IService<TModel, TViewModel> :
        IReadOnlyService<TModel, TViewModel>
        where TModel : class, new()
        where TViewModel : class, IComparableModel<TModel>, new() {

        Task<TViewModel> SaveAsync(TViewModel viewModel, CancellationToken cancellationToken = default);

        Task<TViewModel> CreateAsync(TViewModel viewModel, CancellationToken cancellationToken = default);

        Task<TViewModel> UpdateAsync(TViewModel viewModel, CancellationToken cancellationToken = default);

        Task<TViewModel> DeleteAsync(TViewModel viewModel, CancellationToken cancellationToken = default);
    }
}