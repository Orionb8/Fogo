using System.Collections.Generic;

namespace Fogo.Models {

    public class UserModel {
        public string Id { get; set; }
        public string Phone { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PasswordHash { get; set; }
        public bool IsPhoneConfirmed { get; set; }
        public bool IsLocked { get; set; }

        #region Navigation properties

        public ICollection<UserRoleModel> Roles { get; set; }
        public ICollection<ExecutorModel> Executors { get; set; }
        public ICollection<AdvertiserModel> Advertisers { get; set; }

        #endregion Navigation properties
    }
}