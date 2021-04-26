using Fogo.Models;
using Fogo.Services;
using Fogo.ViewModels;
using Microsoft.Extensions.Logging;

namespace Fogo.Api.Controllers {

    public class RolesController : DefaultController<RoleModel, RoleViewModel> {

        public RolesController(
            ILoggerFactory loggerFactory,
            IService<RoleModel, RoleViewModel> service) :
            base(loggerFactory, service) {
        }
    }
}