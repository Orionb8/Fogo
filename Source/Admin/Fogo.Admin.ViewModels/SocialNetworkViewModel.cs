using Fogo.Models;
using System.Collections.Generic;

namespace Fogo.ViewModels {

    public class SocialNetworkViewModel : DefaultViewModel<SocialNetworkModel> {
        public string Name { get; set; }
        public string Url { get; set; }
        public ICollection<AdvertTypeViewModel> AdvertTypes { get; set; }
    }
}