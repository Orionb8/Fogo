using Fogo.Models;
using System.Collections.Generic;

namespace Fogo.ViewModels {

    public class UserViewModel : DefaultViewModel<UserModel> {
        public string Phone { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PasswordHash { get; set; }
        public bool IsPhoneConfirmed { get; set; }
        public bool IsLocked { get; set; }
        public ICollection<RoleViewModel> Roles { get; set; }
    }
}