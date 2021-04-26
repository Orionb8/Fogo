using Fogo.Models;
using Fogo.Selectors;
using Fogo.ViewModels;
using System;
using System.Linq.Expressions;

namespace Fogo.Mappers {

    public class AdvertTypeMapper :
        ISelector<AdvertTypeModel, AdvertTypeViewModel>,
        IMapper<AdvertTypeModel, AdvertTypeViewModel>,
        IMapper<AdvertTypeViewModel, AdvertTypeModel> {

        public Expression<Func<AdvertTypeModel, AdvertTypeViewModel>> Select =>
            source => new AdvertTypeViewModel {
                Id = source.Id,
                Name = source.Name
            };

        public AdvertTypeViewModel Map(AdvertTypeModel source, AdvertTypeViewModel result) {
            result.Id = source.Id;
            result.Name = source.Name;
            return result;
        }

        public AdvertTypeModel Map(AdvertTypeViewModel source, AdvertTypeModel result) {
            result.Id = source.Id;
            result.Name = source.Name;
            return result;
        }
    }
}