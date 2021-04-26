using System;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace Fogo.ViewModels {

    public abstract class DefaultViewModel<TModel> : ViewModel<TModel> where TModel : class {
        public string Id { get; set; }

        [JsonIgnore]
        public override Expression<Func<TModel, bool>> Comparator => EqulityComparator<TModel>(nameof(Id), Id);
    }
}