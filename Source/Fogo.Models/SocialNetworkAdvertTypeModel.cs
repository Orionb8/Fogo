using System.ComponentModel.DataAnnotations.Schema;

namespace Fogo.Models {

    public class SocialNetworkAdvertTypeModel {
        public string SocialNetworkId { get; set; }
        public string AdvertTypeId { get; set; }

        #region Navigation properties

        [ForeignKey(nameof(SocialNetworkId))]
        public SocialNetworkModel SocialNetwork { get; set; }

        [ForeignKey(nameof(AdvertTypeId))]
        public AdvertTypeModel AdvertType { get; set; }

        #endregion Navigation properties
    }
}