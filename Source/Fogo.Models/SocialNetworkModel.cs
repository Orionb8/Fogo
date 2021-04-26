using System.Collections.Generic;

namespace Fogo.Models {

    /// <summary>
    /// Социальная сеть
    /// </summary>
    public class SocialNetworkModel {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        #region Navigation properties

        /// <summary>
        /// Типы рекламы
        /// </summary>
        public ICollection<SocialNetworkAdvertTypeModel> AdvertTypes { get; set; }

        #endregion Navigation properties
    }
}