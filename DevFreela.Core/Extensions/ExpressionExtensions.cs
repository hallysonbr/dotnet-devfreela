using System;
using System.Linq.Expressions;

namespace DevFreela.Core.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            if (expression1 is null) return expression2;
            if (expression2 is null) return expression1;

            var param = Expression.Parameter(typeof(T), "x");
            var body = Expression.AndAlso(Expression.Invoke(expression1, param), Expression.Invoke(expression2, param));
            return Expression.Lambda<Func<T, bool>>(body, param);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            if (expression1 is null) return expression2;
            if (expression2 is null) return expression1;

            var param = Expression.Parameter(typeof(T), "x");
            var body = Expression.OrElse(Expression.Invoke(expression1, param), Expression.Invoke(expression2, param));
            return Expression.Lambda<Func<T, bool>>(body, param);
        }
    }
}
