using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace Fogo.Filters {

    public sealed class Filter {
        private string _path;
        private string _value;
        private Operation _operation = Operation.Equal;
        private Similarity _similarity = Similarity.Any;
        private Connector _connector = Connector.And;
        private ICollection<Filter> _filters = new Collection<Filter>();
        private bool _isValid = false;
        private bool _isValidated = false;
        private bool _isValueless = false;
        private static readonly FilterBuilder _builder = new FilterBuilder();
        private static readonly FilterValidator _validator = new FilterValidator();
        private static readonly FilterExpresser _expresser = new FilterExpresser();
        private static readonly FilterSerializer _serializer = new FilterSerializer();

        public string Path {
            get => _path;
            set {
                _path = value;
                _isValidated = false;
            }
        }

        public string Value {
            get => _value;
            set {
                _value = value;
                _isValidated = false;
            }
        }

        public Operation Operation {
            get => _operation;
            set {
                if (Enum.IsDefined(typeof(Operation), value)) {
                    _operation = value;
                    _isValueless =
                        _operation == Operation.IsNull ||
                        _operation == Operation.IsEmpty ||
                        _operation == Operation.IsNotNull ||
                        _operation == Operation.IsNotEmpty;
                }
            }
        }

        public Similarity Similarity {
            get => _similarity;
            set {
                if (Enum.IsDefined(typeof(Similarity), value)) {
                    _similarity = value;
                }
            }
        }

        public Connector Connector {
            get => _connector;
            set {
                if (Enum.IsDefined(typeof(Connector), value)) {
                    _connector = value;
                }
            }
        }

        public ICollection<Filter> Filters {
            get => _filters;
            set {
                if (value != null) {
                    _filters = value;
                }
            }
        }

        public bool IsValid => _isValid;
        public bool IsValidated => _isValidated;
        public bool IsValueless => _isValueless;
        public bool HasFilters => _filters.Count > 0;

        [JsonIgnore]
        public IEnumerable<Filter> Validated => _filters.Where(filter => filter.IsValid);

        public Filter Add(params Filter[] filters) {
            foreach (Filter filter in filters) {
                _filters.Add(filter);
            }
            return this;
        }

        public string Serialize() => Serialize(_serializer);

        public string Serialize(IFilterSerializer serializer) => serializer.Serialize(this);

        public bool Validate<TSource>() where TSource : class => Validate<TSource>(_validator);

        public bool Validate<TSource>(IFilterValidator validator) where TSource : class {
            if (!_isValidated) {
                _isValid = validator.Validate<TSource>(this);
                _isValidated = true;
            }
            return _isValid;
        }

        public Expression<Func<TSource, bool>> Express<TSource>() where TSource : class => Express<TSource>(_expresser);

        public Expression<Func<TSource, bool>> Express<TSource>(IFilterExpresser expresser) where TSource : class => expresser.Express<TSource>(this);

        public static Filter Deserialize(string filter) => Deserialize(_serializer, filter);

        public static Filter Deserialize(IFilterDeserializer serializer, string filter) => serializer.Deserialize(filter);

        public static IFilterBuilder SetPath(string path) => _builder.SetPath(path);
    }
}