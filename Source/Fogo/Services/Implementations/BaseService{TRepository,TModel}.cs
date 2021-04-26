using Fogo.Data.Repositories;
using Fogo.Data.Transactions;
using Fogo.Models;
using Fogo.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Fogo.Services.Implementations {

    public class BaseService<TRepository, TModel> :
        ReadOnlyService<TRepository, TModel>,
        IService<TModel>
        where TRepository : IRepository<TModel>
        where TModel : class, IComparableModel<TModel>, new() {

        public BaseService(
            ILoggerFactory loggerFactory,
            TRepository repository) :
            base(loggerFactory, repository) {
        }

        public virtual async Task<TModel> SaveAsync(TModel model, CancellationToken cancellationToken = default) {
            model = await ToModelAsync(model, cancellationToken);
            using ITransaction transaction = await _repository.BeginTransactionAsync(cancellationToken);
            try {
                model = await (await _repository.AnyAsync(model.Comparator, cancellationToken)
                    ? _repository.UpdateAsync(model, cancellationToken)
                    : _repository.CreateAsync(model, cancellationToken));
                await _repository.SaveAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            } catch {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
            return await ToViewModelAsync(model, cancellationToken);
        }

        public virtual async Task<TModel> CreateAsync(TModel model, CancellationToken cancellationToken = default) {
            if (await _repository.AnyAsync(model.Comparator, cancellationToken)) {
                throw new ConflictException();
            }
            model = await ToModelAsync(model, cancellationToken);
            using ITransaction transaction = await _repository.BeginTransactionAsync(cancellationToken);
            try {
                model = await _repository.CreateAsync(model, cancellationToken);
                await _repository.SaveAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            } catch {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
            return await ToViewModelAsync(model, cancellationToken);
        }

        public virtual async Task<TModel> UpdateAsync(TModel model, CancellationToken cancellationToken = default) {
            if (!await _repository.AnyAsync(model.Comparator, cancellationToken)) {
                throw new NotFoundException();
            }
            model = await ToModelAsync(model, cancellationToken);
            using ITransaction transaction = await _repository.BeginTransactionAsync(cancellationToken);
            try {
                model = await _repository.UpdateAsync(model, cancellationToken);
                await _repository.SaveAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            } catch {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
            return await ToViewModelAsync(model, cancellationToken);
        }

        public virtual async Task<TModel> DeleteAsync(TModel model, CancellationToken cancellationToken = default) {
            if (!await _repository.AnyAsync(model.Comparator, cancellationToken)) {
                throw new NotFoundException();
            }
            model = await _repository.SingleAsync(model.Comparator, cancellationToken);
            using ITransaction transaction = await _repository.BeginTransactionAsync(cancellationToken);
            try {
                model = await _repository.DeleteAsync(model, cancellationToken);
                await _repository.SaveAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            } catch {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
            return model;
        }

        protected virtual async Task<TModel> ToModelAsync(TModel model, CancellationToken cancellationToken = default) {
            if (await _repository.AnyAsync(model.Comparator, cancellationToken)) {
                return await _repository.SingleAsync(model.Comparator, cancellationToken);
            }
            return model;
        }

        protected virtual Task<TModel> ToViewModelAsync(TModel model, CancellationToken cancellationToken = default) {
            return Task.FromResult(model);
        }
    }
}