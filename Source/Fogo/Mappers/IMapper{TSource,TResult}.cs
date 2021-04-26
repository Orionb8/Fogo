namespace Fogo.Mappers {

    public interface IMapper<TSource, TResult> where TResult : new() {

        TResult Map(TSource source) => Map(source, new TResult());

        TResult Map(TSource source, TResult result);
    }
}