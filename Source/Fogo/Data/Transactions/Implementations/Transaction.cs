using Microsoft.EntityFrameworkCore.Storage;
using System.Threading;
using System.Threading.Tasks;

namespace Fogo.Data.Transactions.Implementations {

    public class Transaction : Disposable, ITransaction {
        protected readonly IDbContextTransaction _transaction;

        public Transaction(IDbContextTransaction transaction) {
            _transaction = transaction;
        }

        public virtual Task CommitAsync(CancellationToken cancellationToken = default) {
            return _transaction.CommitAsync(cancellationToken);
        }

        public virtual Task RollbackAsync(CancellationToken cancellationToken = default) {
            return _transaction.RollbackAsync(cancellationToken);
        }

        public virtual ValueTask DisposeAsync() {
            return _transaction.DisposeAsync();
        }

        protected override void DisposeUnmanagedResources() {
            _transaction.Dispose();
        }
    }
}