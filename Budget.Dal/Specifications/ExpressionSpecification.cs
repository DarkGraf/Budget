using Budget.Bll.Interfaces;
using System;
using System.Linq.Expressions;

namespace Budget.Dal.Specifications
{
    abstract class ExpressionSpecification<T> : IExpressionSpecification<T>
    {
        private readonly string[] emptyStringArray = new string[0];

        public virtual string[] Includes => emptyStringArray;

        public bool IsSatisfiedBy(T obj)
        {
            return ToExpression().Compile().Invoke(obj);
        }

        public abstract Expression<Func<T, bool>> ToExpression();
    }
}