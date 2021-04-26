using Fogo.Data.Repositories;
using Fogo.Data.Transactions;
using Fogo.Mappers;
using Fogo.Models;
using Fogo.Selectors;
using Fogo.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Fogo.Services.Implementations {

    public class BaseService<TRepository, TModel, TViewModel> :
        ReadOnlyService<TRepository, TModel, TViewModel>,
        IService<TModel, TViewModel>
        where TRepository : IRepository<TModel>
        where TModel : class, new()
        where TViewModel : class, IComparableModel<TModel>, new() {

        public BaseService(
            ILoggerFactory loggerFactory,
            IMapper mapper,
            ISelector selector,
            TRepository repository) :
            base(loggerFactory, mapper, selector, repository) {
        }

        public virtual async Task<TViewModel> SaveAsync(TViewModel viewModel, CancellationToken cancellationToken = default) {
            TModel model = await ToModelAsync(viewModel, cancellationToken);
            using ITransaction transaction = await _repository.BeginTransactionAsync(cancellationToken);
            try {
                model = await (await _repository.AnyAsync(viewModel.Comparator, cancellationToken)
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

        public virtual async Task<TViewModel> CreateAsync(TViewModel viewModel, CancellationToken cancellationToken = default) {
            if (await _repository.AnyAsync(viewModel.Comparator, cancellationToken)) {
                throw new ConflictException();
            }
            TModel model = await ToModelAsync(viewModel, cancellationToken);
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

        public virtual async Task<TViewModel> UpdateAsync(TViewModel viewModel, CancellationToken cancellationToken = default) {
            if (!await _repository.AnyAsync(viewModel.Comparator, cancellationToken)) {
                throw new NotFoundException();
            }
            TModel model = await ToModelAsync(viewModel, cancellationToken);
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

        public virtual async Task<TViewModel> DeleteAsync(TViewModel viewModel, CancellationToken cancellationToken = default) {
            if (!await _repository.AnyAsync(viewModel.Comparator, cancellationToken)) {
                throw new NotFoundException();
            }
            TModel model = await _repository.SingleAsync(viewModel.Comparator, cancellationToken);
            using ITransaction transaction = await _repository.BeginTransactionAsync(cancellationToken);
            try {
                model = await _repository.DeleteAsync(model, cancellationToken);
                await _repository.SaveAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            } catch {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
            return _mapper.Map(model, viewModel);
        }

        protected virtual async Task<TModel> ToModelAsync(TViewModel viewModel, CancellationToken cancellationToken) {
            if (await _repository.AnyAsync(viewModel.Comparator, cancellationToken)) {
                return _mapper.Map(viewModel, await _repository.SingleAsync(viewModel.Comparator, cancellationToken));
            }
            return _mapper.Map(viewModel, new TModel());
        }

        protected virtual Task<TViewModel> ToViewModelAsync(TModel model, CancellationToken cancellationToken) {
            return Task.FromResult(_mapper.Map(model, new TViewModel()));
        }
    }
}