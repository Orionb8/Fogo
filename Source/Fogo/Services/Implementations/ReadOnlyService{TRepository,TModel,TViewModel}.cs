using Fogo.Data.Repositories;
using Fogo.Mappers;
using Fogo.Models;
using Fogo.Requests;
using Fogo.Requests.Extensions;
using Fogo.Selectors;
using Fogo.Selectors.Extensions;
using Fogo.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Fogo.Services.Implementations {

    public class ReadOnlyService<TRepository, TModel, TViewModel> :
        Disposable,
        IReadOnlyService<TModel, TViewModel>
        where TRepository : IRepository<TModel>
        where TModel : class, new()
        where TViewModel : class, IComparableModel<TModel>, new() {
        protected readonly ILogger _logger;
        protected readonly IMapper _mapper;
        protected readonly ISelector _selector;
        protected readonly TRepository _repository;

        public ReadOnlyService(
            ILoggerFactory loggerFactory,
            IMapper mapper,
            ISelector selector,
            TRepository repository) {
            _logger = loggerFactory.CreateLogger(GetType());
            _mapper = mapper;
            _selector = selector;
            _repository = repository;
        }

        public virtual Task<PageResult<TViewModel>> ReadAsync(PageRequest request, CancellationToken cancellationToken = default) => _repository
            .Select<TModel, TViewModel>(_selector)
            .ToPageResultAsync(request, cancellationToken);

        public virtual Task<Result<TViewModel>> ReadAsync(Request request, CancellationToken cancellationToken = default) => _repository
            .Select<TModel, TViewModel>(_selector)
            .ToResultAsync(request, cancellationToken);

        public virtual async Task<TViewModel> ReadAsync(TViewModel viewModel, CancellationToken cancellationToken = default) {
            if (!await _repository.AnyAsync(viewModel.Comparator, cancellationToken)) {
                throw new NotFoundException();
            }
            return await _repository.Where(viewModel.Comparator)
                .Select<TModel, TViewModel>(_selector)
                .SingleAsync(cancellationToken);
        }

        public ValueTask DisposeAsync() => _repository.DisposeAsync();

        protected override void DisposeUnmanagedResources() => _repository.Dispose();
    }
}