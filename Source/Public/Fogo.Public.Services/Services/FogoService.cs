using Fogo.Data.Repositories;
using Fogo.Mappers;
using Fogo.Models;
using Fogo.Selectors;
using Fogo.Services.Implementations;
using Microsoft.Extensions.Logging;

namespace Fogo.Services {

    public class FogoService<TModel, TViewModel> :
        ReadOnlyService<IRepository<TModel>, TModel, TViewModel>,
        IReadOnlyService<TModel, TViewModel>
        where TModel : class, new()
        where TViewModel : class, IComparableModel<TModel>, new() {

        public FogoService(
            ILoggerFactory loggerFactory,
            IMapper mapper,
            ISelector selector,
            IRepository<TModel> repository) :
            base(loggerFactory, mapper, selector, repository) {
        }
    }
}