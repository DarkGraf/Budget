using System;
using System.Linq.Expressions;

namespace Budget.Bll.Interfaces
{
    public interface IExpressionSpecification<T> : ISpecification<T>
    {
        string[] Includes { get; }
        Expression<Func<T, bool>> ToExpression();
    }
}