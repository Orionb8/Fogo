using Fogo.Models;
using System;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace Fogo.ViewModels {

    public class AdvertiserViewModel : DefaultViewModel<AdvertiserModel> {
        public UserViewModel User { get; set; }

        [JsonIgnore]
        public new Expression<Func<AdvertiserModel, bool>> Comparator {
            get => EqulityComparator<AdvertiserModel>(nameof(Id), Id);
        }
    }
}