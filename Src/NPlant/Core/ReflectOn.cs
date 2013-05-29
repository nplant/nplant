using System;
using System.Linq.Expressions;
using System.Reflection;

namespace NPlant.Core
{
    public static class ReflectOn<T>
    {
        public static MemberInfo ForMember<TMember>(Expression<Func<T, TMember>> expression)
        {
            var memberExpression = Find<MemberExpression>(expression);

            MemberInfo member = memberExpression.Member;

            if (member == null)
                throw new ArgumentException("Failed to find the member expression");

            return member;
        }

        private static TMember Find<TMember>(LambdaExpression expression) where TMember : Expression
        {
            Expression body = expression.Body;
            var unaryExpression = body as UnaryExpression;
            return (unaryExpression == null ? body : unaryExpression.Operand) as TMember;
        }
    }
}
