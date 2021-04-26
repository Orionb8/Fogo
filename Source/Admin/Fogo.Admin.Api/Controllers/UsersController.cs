using Fogo.Models;
using Fogo.Services;
using Fogo.ViewModels;
using Microsoft.Extensions.Logging;

namespace Fogo.Api.Controllers {

    public class UsersController : DefaultController<UserModel, UserViewModel> {

        public UsersController(
            ILoggerFactory loggerFactory,
            IService<UserModel, UserViewModel> service) :
            base(loggerFactory, service) {
        }
    }
}