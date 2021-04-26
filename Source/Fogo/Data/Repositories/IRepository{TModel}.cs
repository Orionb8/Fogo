using Fogo.Data.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Fogo.Data.Repositories {

    public interface IRepository<TModel> :
        IQueryable<TModel>,
        IAsyncEnumerable<TModel>,
        IAsyncDisposable,
        IDisposable
        where TModel : class {

        Task<ITransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

        Task<TModel> CreateAsync(TModel model, CancellationToken cancellationToken = default);

        Task<TModel> UpdateAsync(TModel model, CancellationToken cancellationToken = default);

        Task<TModel> DeleteAsync(TModel model, CancellationToken cancellationToken = default);

        Task<bool> SaveAsync(CancellationToken cancellationToken = default);
    }
}