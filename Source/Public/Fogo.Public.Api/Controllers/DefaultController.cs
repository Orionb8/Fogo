using Fogo.Models;
using Fogo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Fogo.Api.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class DefaultController<TModel, TViewModel> :
        ReadOnlyController<IReadOnlyService<TModel, TViewModel>, TModel, TViewModel>
        where TModel : class, new()
        where TViewModel : class, IComparableModel<TModel>, new() {

        protected DefaultController(
            ILoggerFactory loggerFactory,
            IService<TModel, TViewModel> service) :
            base(loggerFactory, service) {
        }
    }
}