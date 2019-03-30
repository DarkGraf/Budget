using Budget.Bll.DomainObjects;

namespace Budget.Bll.Interfaces
{
    public interface ISpecificationFactory
    {
        IExpressionSpecification<FinanceOperation> GetOperationsWithArticles();
    }
}