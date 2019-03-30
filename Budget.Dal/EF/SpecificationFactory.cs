using Budget.Bll.DomainObjects;
using Budget.Bll.Interfaces;
using Budget.Dal.Specifications;

namespace Budget.Dal.EF
{
    class SpecificationFactory : ISpecificationFactory
    {
        public IExpressionSpecification<FinanceOperation> GetOperationsWithArticles()
        {
            return new OperationsWithArticlesSpecification();
        }
    }
}