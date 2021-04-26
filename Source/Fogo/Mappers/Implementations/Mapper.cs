using System;

namespace Fogo.Mappers.Implementations {

    public class Mapper : IMapper {
        protected readonly IServiceProvider _provider;

        public Mapper(IServiceProvider provider) {
            _provider = provider;
        }

        public virtual TResult Map<TSource, TResult>(TSource source) where TResult : new() {
            return GetMapper<TSource, TResult>().Map(source);
        }

        public virtual TResult Map<TSource, TResult>(TSource source, TResult result) where TResult : new() {
            return GetMapper<TSource, TResult>().Map(source, result);
        }

        public virtual IMapper<TSource, TResult> GetMapper<TSource, TResult>() where TResult : new() {
            return _provider.GetService(typeof(IMapper<TSource, TResult>)) as IMapper<TSource, TResult>;
        }
    }
}