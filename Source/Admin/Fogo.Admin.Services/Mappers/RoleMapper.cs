using Fogo.Models;
using Fogo.Selectors;
using Fogo.ViewModels;
using System;
using System.Linq.Expressions;

namespace Fogo.Mappers {

    public class RoleMapper :
        ISelector<RoleModel, RoleViewModel>,
        IMapper<RoleModel, RoleViewModel>,
        IMapper<RoleViewModel, RoleModel> {

        public Expression<Func<RoleModel, RoleViewModel>> Select =>
            source => new RoleViewModel {
                Id = source.Id,
                Name = source.Name
            };

        public RoleViewModel Map(RoleModel source, RoleViewModel result) {
            result.Id = source.Id;
            result.Name = source.Name;
            return result;
        }

        public RoleModel Map(RoleViewModel source, RoleModel result) {
            result.Id = source.Id;
            result.Name = source.Name;
            return result;
        }
    }
}