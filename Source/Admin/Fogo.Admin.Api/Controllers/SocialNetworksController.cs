using Fogo.Models;
using Fogo.Services;
using Fogo.ViewModels;
using Microsoft.Extensions.Logging;

namespace Fogo.Api.Controllers {

    public class SocialNetworksController : DefaultController<SocialNetworkModel, SocialNetworkViewModel> {

        public SocialNetworksController(
            ILoggerFactory loggerFactory,
            IService<SocialNetworkModel, SocialNetworkViewModel> service) :
            base(loggerFactory, service) {
        }
    }
}