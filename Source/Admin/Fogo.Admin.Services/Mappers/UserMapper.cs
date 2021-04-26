using Fogo.Models;
using Fogo.Selectors;
using Fogo.ViewModels;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Fogo.Mappers {

    public class UserMapper :
        ISelector<UserModel, UserViewModel>,
        IMapper<UserModel, UserViewModel>,
        IMapper<UserViewModel, UserModel> {

        public Expression<Func<UserModel, UserViewModel>> Select =>
            source => new UserViewModel {
                Id = source.Id,
                Phone = source.Phone,
                LastName = source.LastName,
                FirstName = source.FirstName,
                PasswordHash = source.PasswordHash,
                IsPhoneConfirmed = source.IsPhoneConfirmed,
                IsLocked = source.IsLocked,
                Roles = source.Roles.Select(source => new RoleViewModel {
                    Id = source.RoleId,
                    Name = source.Role.Name
                }).ToList(),
            };

        public UserViewModel Map(UserModel source, UserViewModel result) {
            result.Id = source.Id;
            result.Phone = source.Phone;
            result.LastName = source.LastName;
            result.FirstName = source.FirstName;
            result.PasswordHash = source.PasswordHash;
            result.IsPhoneConfirmed = source.IsPhoneConfirmed;
            result.IsLocked = source.IsLocked;
            result.Roles = source.Roles?.Select(userRole => new RoleViewModel {
                Id = userRole.RoleId,
                Name = userRole.Role?.Name
            }).ToList();
            return result;
        }

        public UserModel Map(UserViewModel source, UserModel result) {
            result.Id = source.Id;
            result.Phone = source.Phone;
            result.LastName = source.LastName;
            result.FirstName = source.FirstName;
            result.PasswordHash = source.PasswordHash;
            result.IsPhoneConfirmed = source.IsPhoneConfirmed;
            result.IsLocked = source.IsLocked;
            result.Roles = source.Roles?.Select(role => new UserRoleModel {
                UserId = source.Id,
                RoleId = role.Id
            }).ToList();
            return result;
        }
    }
}