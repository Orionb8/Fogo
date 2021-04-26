using Fogo.Data.Repositories;
using Fogo.Models;
using Fogo.Requests;
using Fogo.Requests.Extensions;
using Fogo.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Fogo.Services.Implementations {

    public class ReadOnlyService<TRepository, TModel> :
        Disposable,
        IReadOnlyService<TModel>
        where TRepository : IRepository<TModel>
        where TModel : class, IComparableModel<TModel>, new() {
        protected readonly ILogger _logger;
        protected readonly TRepository _repository;

        public ReadOnlyService(
            ILoggerFactory loggerFactory,
            TRepository repository) {
            _logger = loggerFactory.CreateLogger(GetType());
            _repository = repository;
        }

        public virtual Task<PageResult<TModel>> ReadAsync(PageRequest request, CancellationToken cancellationToken = default) => request
            .ToPageResultAsync(_repository, cancellationToken);

        public virtual Task<Result<TModel>> ReadAsync(Request request, CancellationToken cancellationToken = default) => request
            .ToResultAsync(_repository, cancellationToken);

        public virtual async Task<TModel> ReadAsync(TModel model, CancellationToken cancellationToken = default) {
            if (!await _repository.AnyAsync(model.Comparator, cancellationToken)) {
                throw new NotFoundException();
            }
            return await _repository.SingleAsync(model.Comparator, cancellationToken);
        }

        public ValueTask DisposeAsync() => _repository.DisposeAsync();

        protected override void DisposeUnmanagedResources() => _repository.Dispose();
    }
}