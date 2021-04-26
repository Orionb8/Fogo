using Fogo.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace Fogo.ViewModels {

    public class ExecutorViewModel : DefaultViewModel<ExecutorModel> {
        public UserViewModel User { get; set; }
        public ICollection<ExecutorSocialNetworkViewModel> SocialNetworks { get; set; }

        [JsonIgnore]
        public new Expression<Func<ExecutorModel, bool>> Comparator {
            get => EqulityComparator<ExecutorModel>(nameof(Id), Id);
        }
    }
}