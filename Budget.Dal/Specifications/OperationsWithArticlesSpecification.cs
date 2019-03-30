using Budget.Bll.DomainObjects;
using System;
using System.Linq.Expressions;

namespace Budget.Dal.Specifications
{
    class OperationsWithArticlesSpecification : ExpressionSpecification<FinanceOperation>
    {
        readonly string[] includes = new string[]
        {
            nameof(FinanceOperation.Article)
        };

        public override string[] Includes => includes;

        public override Expression<Func<FinanceOperation, bool>> ToExpression()
        {
            return r => true;
        }
    }
}