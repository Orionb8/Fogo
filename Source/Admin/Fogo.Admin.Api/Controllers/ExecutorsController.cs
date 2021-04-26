using Fogo.Models;
using Fogo.Services;
using Fogo.ViewModels;
using Microsoft.Extensions.Logging;

namespace Fogo.Api.Controllers {

    public class ExecutorsController : DefaultController<ExecutorModel, ExecutorViewModel> {

        public ExecutorsController(
            ILoggerFactory loggerFactory,
            IService<ExecutorModel, ExecutorViewModel> service) :
            base(loggerFactory, service) {
        }
    }
}