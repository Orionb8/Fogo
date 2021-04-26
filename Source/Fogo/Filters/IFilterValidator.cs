namespace Fogo.Filters {

    public interface IFilterValidator {

        bool Validate<TSource>(Filter filter) where TSource : class;
    }
}