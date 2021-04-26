using Fogo.Data.Transactions;
using Fogo.Data.Transactions.Implementations;
using Fogo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Fogo.Data.Repositories.Implementations {

    public class Repository<TDbContext, TModel> :
        Disposable,
        IRepository<TModel>
        where TDbContext : DbContext
        where TModel : class {
        protected TDbContext _context;
        protected DbSet<TModel> _set;
        protected IQueryable<TModel> _queryable;
        protected IAsyncEnumerable<TModel> _enumerable;

        public Repository(TDbContext context) {
            _context = context;
            _set = _context.Set<TModel>();
            _queryable = _set.AsQueryable<TModel>();
            _enumerable = _set.AsAsyncEnumerable<TModel>();
        }

        public Type ElementType => _queryable.ElementType;
        public Expression Expression => _queryable.Expression;
        public IQueryProvider Provider => _queryable.Provider;

        public virtual async Task<ITransaction> BeginTransactionAsync(CancellationToken cancellationToken = default) {
            return new Transaction(await _context.Database.BeginTransactionAsync(cancellationToken));
        }

        public virtual Task<TModel> CreateAsync(TModel model, CancellationToken cancellationToken = default) {
            if (model is ITrackableModel trackable) {
                trackable.CreatedAt = DateTime.Now;
            }
            model = _context.Add(model).Entity;
            return Task.FromResult(model);
        }

        public virtual Task<TModel> UpdateAsync(TModel model, CancellationToken cancellationToken = default) {
            if (model is ITrackableModel trackable) {
                trackable.LastUpdatedAt = DateTime.Now;
            }
            model = _context.Update(model).Entity;
            return Task.FromResult(model);
        }

        public virtual Task<TModel> DeleteAsync(TModel model, CancellationToken cancellationToken = default) {
            if (model is IRecoverableModel recoverable) {
                recoverable.DeletedAt = DateTime.Now;
                recoverable.IsDeleted = true;
            }
            model = model is IRecoverableModel
                ? _context.Update(model).Entity
                : _context.Remove(model).Entity;
            return Task.FromResult(model);
        }

        public virtual async Task<bool> SaveAsync(CancellationToken cancellationToken = default) {
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public IAsyncEnumerator<TModel> GetAsyncEnumerator(CancellationToken cancellationToken = default) {
            return _enumerable.GetAsyncEnumerator(cancellationToken);
        }

        public IEnumerator<TModel> GetEnumerator() {
            return _queryable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public ValueTask DisposeAsync() {
            return _context.DisposeAsync();
        }

        protected override void DisposeUnmanagedResources() {
            _context.Dispose();
        }
    }
}