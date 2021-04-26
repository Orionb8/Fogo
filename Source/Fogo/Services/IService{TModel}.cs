using Fogo.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Fogo.Services {

    public interface IService<TModel> :
        IReadOnlyService<TModel>
        where TModel : class, IComparableModel<TModel>, new() {

        Task<TModel> SaveAsync(TModel model, CancellationToken cancellationToken = default);

        Task<TModel> CreateAsync(TModel model, CancellationToken cancellationToken = default);

        Task<TModel> UpdateAsync(TModel model, CancellationToken cancellationToken = default);

        Task<TModel> DeleteAsync(TModel model, CancellationToken cancellationToken = default);
    }
}