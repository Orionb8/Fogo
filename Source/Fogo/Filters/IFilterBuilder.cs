using System;
using System.Linq.Expressions;

namespace Fogo.Filters {

    public interface IFilterBuilder {

        IFilterBuilder SetPath(string path);

        IFilterBuilder SetValue(string value);

        IFilterBuilder SetOperation(Operation operation);

        IFilterBuilder SetSimilarity(Similarity similarity);

        IFilterBuilder SetConnector(Connector connector);

        IFilterBuilder SetFilters(params Filter[] filters);

        Filter Build();

        string Serialize();

        bool Validate<TSource>() where TSource : class;

        Expression<Func<TSource, bool>> Express<TSource>() where TSource : class;
    }
}