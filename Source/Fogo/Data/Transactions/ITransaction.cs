using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fogo.Data.Transactions {

    public interface ITransaction : IAsyncDisposable, IDisposable {

        Task CommitAsync(CancellationToken cancellationToken = default);

        Task RollbackAsync(CancellationToken cancellationToken = default);
    }
}