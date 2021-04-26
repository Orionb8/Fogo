using System;
using System.Linq.Expressions;

namespace Fogo.Filters {

    public class FilterBuilder : IFilterBuilder {
        protected Filter _filter = new Filter();

        public virtual IFilterBuilder SetPath(string path) {
            _filter.Path = path;
            return this;
        }

        public virtual IFilterBuilder SetValue(string value) {
            _filter.Value = value;
            return this;
        }

        public virtual IFilterBuilder SetOperation(Operation operation) {
            _filter.Operation = operation;
            return this;
        }

        public virtual IFilterBuilder SetSimilarity(Similarity similarity) {
            _filter.Similarity = similarity;
            return this;
        }

        public virtual IFilterBuilder SetConnector(Connector connector) {
            _filter.Connector = connector;
            return this;
        }

        public virtual IFilterBuilder SetFilters(params Filter[] filters) {
            _filter.Add(filters);
            return this;
        }

        public virtual Filter Build() => _filter;

        public virtual string Serialize() => _filter.Serialize();

        public virtual bool Validate<TSource>() where TSource : class => _filter.Validate<TSource>();

        public virtual Expression<Func<TSource, bool>> Express<TSource>() where TSource : class => _filter.Express<TSource>();
    }
}