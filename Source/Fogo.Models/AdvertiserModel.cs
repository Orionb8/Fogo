using System.ComponentModel.DataAnnotations.Schema;

namespace Fogo.Models {

    /// <summary>
    /// Рекламодатель
    /// </summary>
    public class AdvertiserModel {
        public string Id { get; set; }
        public string UserId { get; set; }

        #region Navigation properties

        [ForeignKey(nameof(UserId))]
        public UserModel User { get; set; }

        #endregion Navigation properties
    }
}