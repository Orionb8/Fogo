using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fogo.Models {

    /// <summary>
    /// Исполнитель
    /// </summary>
    public class ExecutorModel {
        public string Id { get; set; }
        public string UserId { get; set; }

        #region Navigation properties

        [ForeignKey(nameof(UserId))]
        public UserModel User { get; set; }

        public ICollection<ExecutorSocialNetworkModel> SocialNetworks { get; set; }

        #endregion Navigation properties
    }
}