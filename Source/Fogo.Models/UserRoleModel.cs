using System.ComponentModel.DataAnnotations.Schema;

namespace Fogo.Models {

    public class UserRoleModel {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        #region Navigation properties

        [ForeignKey(nameof(UserId))]
        public UserModel User { get; set; }

        [ForeignKey(nameof(RoleId))]
        public RoleModel Role { get; set; }

        #endregion Navigation properties
    }
}