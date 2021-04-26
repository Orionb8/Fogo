using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Fogo.Filters {

    public class FilterExpresser : IFilterExpresser {
        protected readonly MethodInfo _contains = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) });
        protected readonly MethodInfo _startsWith = typeof(string).GetMethod(nameof(string.StartsWith), new[] { typeof(string) });
        protected readonly MethodInfo _endsWith = typeof(string).GetMethod(nameof(string.EndsWith), new[] { typeof(string) });

        public Expression<Func<TSource, bool>> Express<TSource>(Filter filter) where TSource : class {
            ParameterExpression parameter = Expression.Parameter(typeof(TSource), "x");
            return Expression.Lambda<Func<TSource, bool>>(Express<TSource>(filter, parameter), parameter);
        }

        protected virtual Expression Express<TSource>(Filter filter, Expression parameter) where TSource : class {
            Expression expression = Expression.Constant(true);

            if (filter.Validate<TSource>()) {
                Stack<Property> properties = new Stack<Property>();

                foreach (string name in filter.Path.Split(".", StringSplitOptions.RemoveEmptyEntries)) {
                    Property property = new Property(parameter.Type, name, Expression.Property(parameter, name));

                    parameter = property.IsEnumerable
                        ? Expression.Parameter(property.GenericType, "x")
                        : property.Expression;

                    if (property.IsEnumerable || property.IsValueType) {
                        properties.Push(property);
                    }
                }

                bool isApplied = false;

                foreach (Property property in properties) {
                    if (property.IsEnumerable) {
                        string similarity = filter.Similarity == Similarity.Any || isApplied
                            ? nameof(Enumerable.Any)
                            : nameof(Enumerable.All);

                        MethodInfo method = typeof(Enumerable)
                            .GetMethods(BindingFlags.Public | BindingFlags.Static)
                            .First(method => method.Name == similarity && method.GetParameters().Length == 2)
                            .MakeGenericMethod(property.GenericType);

                        expression = Expression.Lambda(expression, Expression.Parameter(property.GenericType, "x"));
                        expression = Expression.Call(method, property.Expression, expression);

                        if (!isApplied) {
                            isApplied = true;
                        }
                    } else {
                        TypeConverter converter = TypeDescriptor.GetConverter(property.GenericType);
                        object value = converter.ConvertFrom(filter.Value);
                        Expression constant = Expression.Constant(value);
                        expression = Express<TSource>(filter, property.Expression, constant);
                    }
                }
            }

            return ExpressFilters();

            Expression ExpressFilters() {
                if (!filter.HasFilters) {
                    return expression;
                }

                bool isValid = filter.Validate<TSource>();

                foreach (Filter filter in filter.Validated) {
                    if (!isValid) {
                        expression = Express<TSource>(filter, parameter);
                        isValid = true;
                    } else {
                        expression = filter.Connector == Connector.And
                            ? Expression.AndAlso(expression, Express<TSource>(filter, parameter))
                            : Expression.OrElse(expression, Express<TSource>(filter, parameter));
                    }
                }

                return expression;
            }
        }

        protected virtual Expression Express<TSource>(Filter filter, Expression left, Expression right) where TSource : class => filter.Operation switch {
            Operation.NotEqual => Expression.NotEqual(left, right),
            Operation.LessThan => Expression.LessThan(left, right),
            Operation.GreaterThan => Expression.GreaterThan(left, right),
            Operation.LessThanOrEqual => Expression.LessThanOrEqual(left, right),
            Operation.GreaterThanOrEqual => Expression.GreaterThanOrEqual(left, right),
            Operation.Contains => Expression.Call(left, _contains, right),
            Operation.NotContains => Expression.Not(Expression.Call(left, _contains, right)),
            Operation.StartsWith => Expression.Call(left, _startsWith, right),
            Operation.NotStartsWith => Expression.Not(Expression.Call(left, _startsWith, right)),
            Operation.EndsWith => Expression.Call(left, _endsWith, right),
            Operation.NotEndsWith => Expression.Not(Expression.Call(left, _endsWith, right)),
            Operation.IsNull => Expression.Equal(left, Expression.Constant(null)),
            Operation.IsNotNull => Expression.NotEqual(left, Expression.Constant(null)),
            Operation.IsEmpty => Expression.Equal(left, Expression.Constant(string.Empty)),
            Operation.IsNotEmpty => Expression.NotEqual(left, Expression.Constant(string.Empty)),
            _ => Expression.Equal(left, right),
        };

        protected class Property {

            public Property(
                Type instanceType,
                string propertyName,
                Expression expression) {
                PropertyName = propertyName;
                InstanceType = instanceType;
                Expression = expression;
                PropertyType = InstanceType.GetProperty(PropertyName, BindingFlags.Public | BindingFlags.Instance).PropertyType;
                IsNullable = PropertyType.IsGenericType && PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>);
                IsEnumerable = PropertyType.IsGenericType && typeof(IEnumerable).IsAssignableFrom(PropertyType);
                GenericType = IsEnumerable || IsNullable ? PropertyType.GenericTypeArguments[0] : PropertyType;
                IsValueType = GenericType == typeof(string) || GenericType.IsValueType;
            }

            /// <summary>
            /// <see cref="MemberExpression"/>
            /// </summary>
            public Expression Expression { get; }

            public string PropertyName { get; }
            public Type InstanceType { get; }
            public Type PropertyType { get; }
            public Type GenericType { get; }
            public bool IsValueType { get; }
            public bool IsNullable { get; }
            public bool IsEnumerable { get; }
        }
    }
}