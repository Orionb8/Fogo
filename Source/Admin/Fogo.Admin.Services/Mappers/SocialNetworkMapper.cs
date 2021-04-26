using Fogo.Models;
using Fogo.Selectors;
using Fogo.ViewModels;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Fogo.Mappers {

    public class SocialNetworkMapper :
        ISelector<SocialNetworkModel, SocialNetworkViewModel>,
        IMapper<SocialNetworkModel, SocialNetworkViewModel>,
        IMapper<SocialNetworkViewModel, SocialNetworkModel> {

        public Expression<Func<SocialNetworkModel, SocialNetworkViewModel>> Select =>
            source => new SocialNetworkViewModel {
                Id = source.Id,
                Name = source.Name,
                Url = source.Url,
                AdvertTypes = source.AdvertTypes.Select(socialNetworkAdvertType => new AdvertTypeViewModel {
                    Id = socialNetworkAdvertType.AdvertTypeId,
                    Name = socialNetworkAdvertType.AdvertType.Name
                }).ToList()
            };

        public SocialNetworkViewModel Map(SocialNetworkModel source, SocialNetworkViewModel result) {
            result.Id = source.Id;
            result.Name = source.Id;
            result.Url = source.Url;
            result.AdvertTypes = source.AdvertTypes?.Select(socialNetworkAdvertType => new AdvertTypeViewModel {
                Id = socialNetworkAdvertType.AdvertTypeId,
                Name = socialNetworkAdvertType.AdvertType?.Name
            }).ToList();
            return result;
        }

        public SocialNetworkModel Map(SocialNetworkViewModel source, SocialNetworkModel result) {
            result.Id = source.Id;
            result.Name = source.Id;
            result.Url = source.Url;
            result.AdvertTypes = source.AdvertTypes?.Select(advertType => new SocialNetworkAdvertTypeModel {
                SocialNetworkId = source.Id,
                AdvertTypeId = advertType.Id
            }).ToList();
            return result;
        }
    }
}