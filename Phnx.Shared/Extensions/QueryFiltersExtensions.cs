using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Phnx.Shared.Extensions
{
    public static class QueryFiltersExtensions
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T));

            BinaryExpression body = Expression.AndAlso(
                Expression.Invoke(expr1, parameter),
                Expression.Invoke(expr2, parameter)
            );

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            ParameterExpression parameter1 = expr1.Parameters[0];
            ReplaceParameterVisitor visitor = new(expr2.Parameters[0], parameter1);
            Expression body2WithParam1 = visitor.Visit(expr2.Body);
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, body2WithParam1), parameter1);
        }

        private class ReplaceParameterVisitor(ParameterExpression oldParameter, ParameterExpression newParameter) : ExpressionVisitor
        {
            protected override Expression VisitParameter(ParameterExpression node)
            {
                if (ReferenceEquals(node, oldParameter))
                    return newParameter;

                return base.VisitParameter(node);
            }
        }
    }
}