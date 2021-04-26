using Fogo.Models;
using Fogo.Selectors;
using Fogo.ViewModels;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Fogo.Mappers {

    public class ExecutorMapper :
        ISelector<ExecutorModel, ExecutorViewModel>,
        IMapper<ExecutorModel, ExecutorViewModel>,
        IMapper<ExecutorViewModel, ExecutorModel> {
        private readonly IMapper _mapper;

        public ExecutorMapper(IMapper mapper) {
            _mapper = mapper;
        }

        public Expression<Func<ExecutorModel, ExecutorViewModel>> Select =>
            source => new ExecutorViewModel {
                Id = source.Id,
                User = new UserViewModel {
                    Id = source.User.Id,
                    Phone = source.User.Phone,
                    LastName = source.User.LastName,
                    FirstName = source.User.FirstName,
                    PasswordHash = source.User.PasswordHash,
                    IsPhoneConfirmed = source.User.IsPhoneConfirmed,
                    IsLocked = source.User.IsLocked,
                    Roles = source.User.Roles.Select(userRole => new RoleViewModel {
                        Id = userRole.RoleId,
                        Name = userRole.Role.Name
                    }).ToList(),
                }
            };

        public ExecutorViewModel Map(ExecutorModel source, ExecutorViewModel result) {
            result.Id = source.Id;
            if (source.User != null) {
                if (result.User == null) {
                    result.User = new UserViewModel();
                }
                _mapper.Map(source.User, result.User);
            }
            result.SocialNetworks = source.SocialNetworks?.Select(executorSocialNetwork => new ExecutorSocialNetworkViewModel {
                SocialNetwork = _mapper.Map(executorSocialNetwork.SocialNetwork, new SocialNetworkViewModel()),
                Url = executorSocialNetwork.Url
            }).ToList();
            return result;
        }

        public ExecutorModel Map(ExecutorViewModel source, ExecutorModel result) {
            result.Id = source.Id;
            if (source.User != null) {
                if (result.User == null) {
                    result.User = new UserModel();
                }
                _mapper.Map(source.User, result.User);
            }
            result.SocialNetworks = source.SocialNetworks?.Select(socialNetwork => new ExecutorSocialNetworkModel {
                Id = socialNetwork.Id,
                ExecutorId = source.Id,
                SocialNetworkId = socialNetwork.SocialNetwork?.Id
            }).ToList();
            return result;
        }
    }
}