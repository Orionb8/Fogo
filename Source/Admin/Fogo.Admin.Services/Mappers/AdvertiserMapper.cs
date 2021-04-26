using Fogo.Models;
using Fogo.Selectors;
using Fogo.ViewModels;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Fogo.Mappers {

    public class AdvertiserMapper :
        ISelector<AdvertiserModel, AdvertiserViewModel>,
        IMapper<AdvertiserModel, AdvertiserViewModel>,
        IMapper<AdvertiserViewModel, AdvertiserModel> {
        private readonly IMapper _mapper;

        public AdvertiserMapper(IMapper mapper) {
            _mapper = mapper;
        }

        public Expression<Func<AdvertiserModel, AdvertiserViewModel>> Select =>
            source => new AdvertiserViewModel {
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

        public AdvertiserViewModel Map(AdvertiserModel source, AdvertiserViewModel result) {
            result.Id = source.Id;
            if (source.User != null) {
                if (result.User == null) {
                    result.User = new UserViewModel();
                }
                _mapper.Map(source.User, result.User);
            }
            return result;
        }

        public AdvertiserModel Map(AdvertiserViewModel source, AdvertiserModel result) {
            result.Id = source.Id;
            if (source.User != null) {
                if (result.User == null) {
                    result.User = new UserModel();
                }
                _mapper.Map(source.User, result.User);
            }
            return result;
        }
    }
}