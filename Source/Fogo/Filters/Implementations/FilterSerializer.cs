using System.Text.Json;

namespace Fogo.Filters {

    public class FilterSerializer : IFilterSerializer, IFilterDeserializer {

        public virtual string Serialize(Filter filter) => JsonSerializer.Serialize(filter);

        public virtual Filter Deserialize(string filter) => JsonSerializer.Deserialize<Filter>(filter);
    }
}