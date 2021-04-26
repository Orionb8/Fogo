using Fogo.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Linq.Expressions;

namespace Fogo.Data.Extensions {

    public static class ModelBuilderExtensions {

        public static void OnModelCreating(this ModelBuilder modelBuilder) {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()) {
                if (typeof(IRecoverableModel).IsAssignableFrom(entityType.ClrType)) {
                    LambdaExpression filter = BuildExpression(entityType.ClrType);
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
                }
            }
        }

        private static LambdaExpression BuildExpression(Type type) {
            ParameterExpression parameter = Expression.Parameter(type, "x");
            MemberExpression member = Expression.Property(parameter, nameof(IRecoverableModel.IsDeleted));
            BinaryExpression body = Expression.Equal(member, Expression.Constant(false));
            Type delegateType = Expression.GetDelegateType(type, typeof(bool));
            return Expression.Lambda(delegateType, body, parameter);
        }
    }
}