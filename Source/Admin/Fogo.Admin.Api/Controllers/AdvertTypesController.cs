using Fogo.Models;
using Fogo.Services;
using Fogo.ViewModels;
using Microsoft.Extensions.Logging;

namespace Fogo.Api.Controllers {

    public class AdvertTypesController : DefaultController<AdvertTypeModel, AdvertTypeViewModel> {

        public AdvertTypesController(
            ILoggerFactory loggerFactory,
            IService<AdvertTypeModel, AdvertTypeViewModel> service) :
            base(loggerFactory, service) {
        }
    }
}