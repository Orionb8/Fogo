using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace Fogo.Filters {

    public class FilterValidator : IFilterValidator {

        public virtual bool Validate<TSource>(Filter filter) where TSource : class {
            Type type = typeof(TSource);

            return IsValidProperty()
                && IsValidValue();

            bool IsValidProperty() {
                if (filter.Path is null) {
                    return false;
                }
                foreach (string property in filter.Path.Split('.', StringSplitOptions.RemoveEmptyEntries)) {
                    type = type.GetProperty(property, BindingFlags.Public | BindingFlags.Instance)?.PropertyType;
                    if (type is null) {
                        return false;
                    }
                    if (type.IsGenericType && typeof(IEnumerable).IsAssignableFrom(type)) {
                        type = type.GenericTypeArguments[0];
                    }
                }
                if (type != typeof(string)) {
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)) {
                        type = type.GenericTypeArguments[0];
                    }
                    if (!type.IsValueType) {
                        return false;
                    }
                }
                return true;
            }

            bool IsValidValue() {
                if (filter.IsValueless) {
                    return true;
                }
                if (filter.Value is null) {
                    return false;
                }
                if (type == typeof(string)) {
                    return true;
                }
                TypeConverter converter = TypeDescriptor.GetConverter(type);
                if (!converter.CanConvertFrom(typeof(string))) {
                    return false;
                }
                try {
                    converter.ConvertFrom(filter.Value);
                } catch {
                    return false;
                }
                return true;
            }
        }
    }
}