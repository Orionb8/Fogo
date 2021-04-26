namespace Fogo.Filters {

    public interface IFilterDeserializer {

        Filter Deserialize(string filter);
    }
}