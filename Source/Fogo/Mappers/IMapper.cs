namespace Fogo.Mappers {

    public interface IMapper {

        TResult Map<TSource, TResult>(TSource source) where TResult : new();

        TResult Map<TSource, TResult>(TSource source, TResult result) where TResult : new();

        IMapper<TSource, TResult> GetMapper<TSource, TResult>() where TResult : new();
    }
}