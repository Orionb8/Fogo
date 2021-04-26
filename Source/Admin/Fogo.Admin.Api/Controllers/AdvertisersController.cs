using Fogo.Models;
using Fogo.Services;
using Fogo.ViewModels;
using Microsoft.Extensions.Logging;

namespace Fogo.Api.Controllers {

    public class AdvertisersController : DefaultController<AdvertiserModel, AdvertiserViewModel> {

        public AdvertisersController(
            ILoggerFactory loggerFactory,
            IService<AdvertiserModel, AdvertiserViewModel> service) :
            base(loggerFactory, service) {
        }
    }
}